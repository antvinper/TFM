using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;

[Serializable]
public class CharacterMutableModel : ICharacterModel
{
    [SerializeField] private StatsTree tree;
    [SerializeField] private StatsDefinition statsDefinitions;
    private int soulFragments;
    private int rupees;

    /**
     * HACER LAS VARIABLES PÚBLICAS QUE SERÁN DE JSON PROPERTY?
     * o
     * LLEVARLO A UNA CLASE APARTE?
     */
    private List<Stat> baseStats;
    private List<Stat> instantStatsModifyPermanent;
    private List<Stat> instantStatsModifyInRun;
    private List<Stat> instantStatsModifyPercentageInRun;

    [JsonIgnore] private List<TimeEffectDefinition> timeEffectDefinitions = new List<TimeEffectDefinition>();

    [JsonIgnore]
    [HideInInspector]
    public List<TimeEffectDefinition> TimeEffectDefinitions { get => timeEffectDefinitions; }

    public StatsTree Tree
    {
        get { return tree; }
    }
    public int Rupees
    {
        get => rupees;
    }
    public int SoulFragments
    {
        get => soulFragments;
    }

    public void Setup()
    {
        baseStats = new List<Stat>();
        instantStatsModifyPermanent = new List<Stat>();
        instantStatsModifyInRun = new List<Stat>();
        instantStatsModifyPercentageInRun = new List<Stat>();

        foreach(StatDefinition sd in statsDefinitions.stats)
        {
            baseStats.Add(new Stat(sd.name, sd.maxValue, sd.minValue, sd.value, sd.actualMaxValue));

            instantStatsModifyInRun.Add(new Stat(sd.name));
            instantStatsModifyPercentageInRun.Add(new Stat(sd.name));
            instantStatsModifyPermanent.Add(new Stat(sd.name));
        }
    }


    
    
    //Se le pasa el valor máximo del stat?
    //Creo una función recursiva?
    private int CalculateStat(StatsEnum stat)
    {
        float value1 = GetStatValueFromList(baseStats, stat) + GetStatValueFromList(instantStatsModifyPermanent, stat);
        float value2 = GetStatValueFromList(instantStatsModifyInRun, stat);
        float value3 = 1 + (GetStatValueFromList(instantStatsModifyPercentageInRun, stat) * 0.01f);
        float valueCalculated = (value1 + value2) * value3;

        valueCalculated = Mathf.Clamp(valueCalculated, baseStats.Find(t => t.Name.Equals(stat)).MinValue, GetActualMaxStatValue(stat));

        //return Math.Clamp(valueCalculated, 0, MaxStatsValues.GetStat(stat));
        return (int)valueCalculated;
    }

    private int GetStatValueFromList(List<Stat> list, StatsEnum stat)
    {
        return list.Find(t => t.Name.Equals(stat)).Value;
    }
    private int GetActualMaxStatValueFromList(List<Stat> list, StatsEnum stat)
    {
        return list.Find(t => t.Name.Equals(stat)).ActualMaxValue;
    }
    private int GetMaxStatValueFromList(List<Stat> list, StatsEnum stat)
    {
        return list.Find(t => t.Name.Equals(stat)).MaxValue;
    }
    private int GetActualMinStatValueFromList(List<Stat> list, StatsEnum stat)
    {
        return list.Find(t => t.Name.Equals(stat)).MinValue;
    }

    //Servirá para feedback en la UI sobre todo
    public int GetActualMaxStatValue(StatsEnum stat)
    {
        float maxValue1 = GetActualMaxStatValueFromList(baseStats, stat) + GetActualMaxStatValueFromList(instantStatsModifyPermanent, stat);
        float maxValue2 = GetActualMaxStatValueFromList(instantStatsModifyInRun, stat);
        float maxValue3 = 1 + (GetActualMaxStatValueFromList(instantStatsModifyPercentageInRun, stat) * 0.01f);
        float actualMaxValueCalculated = (maxValue1 + maxValue2) * maxValue3;

        return (int)actualMaxValueCalculated;
    }
    //Si es porcentual, siempre se devuelve el actualMaxValue
    public int GetStatValue(StatsEnum stat)
    {
        int statValue;
        if (stat.Equals(StatsEnum.MAX_HEALTH))
        {
            statValue = GetActualMaxStatValue(StatsEnum.HEALTH);
        }
        else
        {
            statValue = CalculateStat(stat);
        }
        return statValue;
    }
    
    public int GetActualBaseMaxStat(StatsEnum stat)
    {
        return baseStats.Find(t => t.Name.Equals(stat)).ActualMaxValue;
    }
    /*public int GetBaseMinStat(StatsEnum stat)
    {

    }*/
    
    

    public void PerformApplyPermanentStat(StatModificator statModificator)
    {
        if (statModificator.StatToModify.Equals(StatsEnum.MAX_HEALTH))
        {
            instantStatsModifyPermanent.Find(t => t.Name.Equals(StatsEnum.HEALTH)).IncrementActualMaxValue(statModificator.Value);
        }
        else
        {
            instantStatsModifyPermanent.Find(t => t.Name.Equals(statModificator.StatToModify)).IncrementValue(statModificator.Value);
        }
    }

    /**
     * TODO refactor
     * 
     * Para poder incremental cualquier maxValue
     * También habría que añadir un nuevo check en los efectos que haga referencia
     * a los max values. Así el statusEnum dejaría de tener MAX_HEALTH y no
     * habría que hardcodear aquí el statEnum Health
     */
    public void PerformApplyStatModifyInRun(StatModificator statModificator)
    {
        if (statModificator.StatToModify.Equals(StatsEnum.MAX_HEALTH))
        {
            if (statModificator.IsPercentual)
            {
                instantStatsModifyPercentageInRun.Find(t => t.Name.Equals(StatsEnum.HEALTH)).IncrementActualMaxValue(statModificator.Value);
            }
            else
            {
                instantStatsModifyInRun.Find(t => t.Name.Equals(StatsEnum.HEALTH)).IncrementActualMaxValue(statModificator.Value);
            }
        }
        else
        {
            if (statModificator.IsPercentual)
            {
                instantStatsModifyPercentageInRun.Find(t => t.Name.Equals(statModificator.StatToModify)).IncrementValue(statModificator.Value);
            }
            else
            {
                instantStatsModifyInRun.Find(t => t.Name.Equals(statModificator.StatToModify)).IncrementValue(statModificator.Value);
            }
        }
        
    }

    public void PerformRealHealthChange(StatModificator statModificator)
    {
        if(statModificator.Value > 0)
        {
            Heal(statModificator);
        }
        else
        {
            if (statModificator.BuffDebuffType.Equals(EffectTypes.POISON))
            {
                TakeRealDamage(statModificator);
            }
            else if (statModificator.IsAttack)
            {
                TakeDamage(statModificator);
            }
        }
    }
    public void PerformPercentualHealthChange(StatModificator statModificator)
    {
        if(statModificator.IsAttack)
        {
            TakeRealDamage(statModificator);
        }
        else
        {
            Heal(statModificator);
        }
    }


    /**
     * TODO ?
     * Si el TakeDamage fuese diferente en el personaje y en los enemigos
     * debe implementarse en cada uno por separado
     */
    public void TakeDamage(StatModificator statModificator)
    {
        
        int defense = GetStatValue(StatsEnum.DEFENSE);
        int realDamage = Math.Abs(statModificator.Value) - defense;
        int finalDamage = realDamage < 0 ? 0 : realDamage;

        statModificator.Value = -finalDamage;

        PerformApplyStatModifyInRun(statModificator);

        statModificator.IsAlive = CheckIsAlive();
    }

    public void TakeRealDamage(StatModificator statModificator)
    {
        PerformApplyStatModifyInRun(statModificator);
        statModificator.IsAlive = CheckIsAlive();
    }

    public void Heal(StatModificator statModificator)
    {
        int maxHealth = GetActualBaseMaxStat(statModificator.StatToModify);
        int health = GetStatValue(statModificator.StatToModify);
        int finalHeal = Math.Clamp(statModificator.Value, 0, maxHealth - health);

        statModificator.Value = finalHeal;
        PerformApplyStatModifyInRun(statModificator);
    }

    private bool CheckIsAlive()
    {
        /**
         * TODO
         * Debería llamar a algún evento de muerte??
         */
        return GetStatValue(StatsEnum.HEALTH) > 0;
    }


}
