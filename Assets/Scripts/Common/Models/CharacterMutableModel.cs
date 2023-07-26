using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;

[Serializable]
public class CharacterMutableModel : ICharacterModel
{
    [SerializeField] private StatsTree tree;
    [SerializeField] private StatsDefinition statsDefinitions;

    /**
     * HACER LAS VARIABLES PÚBLICAS QUE SERÁN DE JSON PROPERTY?
     * o
     * LLEVARLO A UNA CLASE APARTE?
     */
    private List<Stat> baseStats;
    private List<StatMin> instantStatsMinModifyPermanent;
    private List<StatMin> instantStatsMinModifyInRun;
    private List<StatMin> instantStatsMinModifyPercentageInRun;


    public StatsTree Tree
    {
        get { return tree; }
    }

    public void Setup()
    {
        baseStats = new List<Stat>();
        instantStatsMinModifyPermanent = new List<StatMin>();
        instantStatsMinModifyInRun = new List<StatMin>();
        instantStatsMinModifyPercentageInRun = new List<StatMin>();

        foreach(StatDefinition sd in statsDefinitions.stats)
        {
            baseStats.Add(new Stat(sd.name, sd.maxValue, sd.minValue, sd.value, sd.actualMaxValue));

            instantStatsMinModifyInRun.Add(new StatMin(sd.name));
            instantStatsMinModifyPercentageInRun.Add(new StatMin(sd.name));
            instantStatsMinModifyPermanent.Add(new StatMin(sd.name));
        }
    }


    [JsonIgnore] private List<TimeEffectDefinition> timeEffectDefinitions = new List<TimeEffectDefinition>();

    [JsonIgnore]
    [HideInInspector]
    public List<TimeEffectDefinition> TimeEffectDefinitions { get => timeEffectDefinitions; }
    
    private float CalculateStat(StatsEnum stat, int baseStat)
    {
        float value1 = baseStat + GetStatValue(instantStatsMinModifyPermanent, stat);
        float value2 = GetStatValue(instantStatsMinModifyInRun, stat);
        float value3 = GetStatValue(instantStatsMinModifyPercentageInRun, stat);

        float valueCalculated = (value1 + value2) * (1 + (value3 * 0.01f));

        //return Math.Clamp(valueCalculated, 0, MaxStatsValues.GetStat(stat));
        return valueCalculated;
    }

    private int GetStatValue(List<StatMin> list, StatsEnum stat)
    {
        return list.Find(t => t.Name.Equals(stat)).Value;
    }
    public int GetStat(StatsEnum stat)
    {
        return (int)CalculateStat(stat, baseStats.Find(t => t.Name.Equals(stat)).Value);
    }
    public int GetActualMaxStat(StatsEnum stat)
    {
        return baseStats.Find(t => t.Name.Equals(stat)).ActualMaxValue;
    }
    

    public void PerformApplyPermanentStat(StatModificator statModificator)
    {
        instantStatsMinModifyPermanent.Find(t => t.Name.Equals(statModificator.StatToModify)).AddValue(statModificator.Value);
    }

    public void PerformApplyStatModifyInRun(StatModificator statModificator)
    {
        if (statModificator.IsPercentual)
        {
            instantStatsMinModifyPercentageInRun.Find(t => t.Name.Equals(statModificator.StatToModify)).AddValue(statModificator.Value);
        }
        else
        {
            instantStatsMinModifyInRun.Find(t => t.Name.Equals(statModificator.StatToModify)).AddValue(statModificator.Value);
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
        
        int defense = GetStat(StatsEnum.DEFENSE);
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
        int maxHealth = GetActualMaxStat(statModificator.StatToModify);
        int health = GetStat(statModificator.StatToModify);
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
        return GetStat(StatsEnum.HEALTH) > 0;
    }


}
