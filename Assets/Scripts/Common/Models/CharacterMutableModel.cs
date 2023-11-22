using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using CompanyStats;
using System.Linq;

[Serializable]
public class CharacterMutableModel// : ICharacterModel
{
    private CharacterStatsDefinition characterStatsDefinition;
    private List<Stat> stats;

    private int soulFragments;
    private int rupees;


    private List<Stat> instantStatsModifyPermanent;
    [JsonProperty]
    public List<Stat> InstantStatsModifyPermanent
    {
        get => instantStatsModifyPermanent;
    }
    private List<Stat> instantStatsModifyInRun;
    private List<Stat> instantStatsModifyPercentageInRun;

    [JsonIgnore] private List<OverTimeEffectDefinition> overTimeEffects;
    [JsonIgnore] private List<DuringTimeEffectDefinition> duringTimeEffects;
    [JsonIgnore] private List<InstantEffectPermanentDefinition> permanentEffects;
    [JsonIgnore] private List<InstantEffectTemporallyDefinition> temporallyEffects;

    [JsonIgnore] public List<OverTimeEffectDefinition> OverTimeEffects { get => overTimeEffects; }
    [JsonIgnore] public List<DuringTimeEffectDefinition> DuringTimeEffects { get => duringTimeEffects; }
    [JsonIgnore] public List<InstantEffectPermanentDefinition> PermanentEffects { get => permanentEffects; }
    [JsonIgnore] public List<InstantEffectTemporallyDefinition> TemporallyEffects { get => temporallyEffects; }

    [JsonProperty]
    public int Rupees
    {
        get => rupees;
        set => rupees = value;
    }
    [JsonProperty]
    public int SoulFragments
    {
        get => soulFragments;
        set => soulFragments = value;
    }

    public void Setup(CharacterStatsDefinition characterStatsDefinition)
    {
        this.characterStatsDefinition = characterStatsDefinition;
        stats = new List<Stat>();
        overTimeEffects = new List<OverTimeEffectDefinition>();
        duringTimeEffects = new List<DuringTimeEffectDefinition>();
        permanentEffects = new List<InstantEffectPermanentDefinition>();
        temporallyEffects = new List<InstantEffectTemporallyDefinition>();
        /*baseStats = new List<Stat>();
        instantStatsModifyPermanent = new List<Stat>();
        instantStatsModifyInRun = new List<Stat>();
        instantStatsModifyPercentageInRun = new List<Stat>();*/

        foreach(StatDefinition sd in characterStatsDefinition.statsDefinition.stats)
        {
            Stat stat = new Stat(sd.name, sd.value, sd.actualMaxValue, sd.maxValue, sd.minValue, sd.isVolatil);
            stats.Add(stat);

            /*baseStats.Add(new Stat(sd.name, sd.maxValue, sd.minValue, sd.value, sd.actualMaxValue));

            instantStatsModifyInRun.Add(new Stat(sd.name));
            instantStatsModifyPercentageInRun.Add(new Stat(sd.name));
            instantStatsModifyPermanent.Add(new Stat(sd.name));*/
        }
    }

    public Stat GetStatFromName(StatNames statName)
    {
        return stats.Where(stat => stat.StatName.Equals(statName)).FirstOrDefault();
    }
    public int GetStatValue(StatNames statName, StatParts statPart)
    {
        int value = 0;
        Stat stat = GetStatFromName(statName);

        switch (statPart)
        {
            case StatParts.MIN_VALUE:
                value = stat.MinValue;
                break;
            case StatParts.MAX_VALUE:
                value = stat.MaxValue;
                break;
            case StatParts.ACTUAL_VALUE:
                value = stat.Value;
                break;
            case StatParts.ACTUAL_MAX_VALUE:
                value = stat.ActualMaxValue;
                break;
        }

        return value;
    }

    /**
     * 2 opciones:
     * - Calcular los stats cada vez que se quiera obtener uno. Más pesado
     * - Calcular los stats cada vez que se añade un efecto.
     * MAKE IT PRIVATE!
     */
    public void CalculateStat(StatNames statName, StatParts statPart)
    {
        GetStatFromName(statName).ResetStat();


        /**
         * 1º Calcular valor añadido no porcentual permanente y temporal
         * 2º Calcular valor añadido no porcentual de los time effects
         * 3º Calcular valor añadido porcentual (en porcentaje) permanente y temporal
         * 4º Calcular valor añadido porcentual de los time effects
         * 
         * 5º Sumar todos los no porcentuales
         * 6º Sumar todos los porcentuales
         * 
         * 7º añadir los no porcentuales
         * 8º añadir los porcentuales
         */
        int notPercentualValue = 0;
        int percentualValue = 0;


        /*
         * 1º se calculan los cambios permanentes no porcentuales
         */
        List<InstantEffectPermanentDefinition> permEffectsRealValue = permanentEffects.Where(e => e.StatAffected.Equals(statName) && !e.IsValueInPercentage).ToList();
        foreach (InstantEffectPermanentDefinition permanentEffect in permEffectsRealValue)
        {

            notPercentualValue += permanentEffect.GetRealValue();
            ChangeStat(permanentEffect);
        }

        /*
         * 2º Se calculan los cambios temporales no porcentuales
         */
        List<InstantEffectTemporallyDefinition> tempEffectsRealValue = temporallyEffects.Where(e => e.StatAffected.Equals(statName) && !e.IsValueInPercentage).ToList();
        foreach (InstantEffectTemporallyDefinition temporallyEffect in tempEffectsRealValue)
        {
            //notPercentualValue += temporallyEffect.GetRealValue();
            ChangeStat(temporallyEffect);
        }

        /*
         * 3º se calculan los cambios permanentes porcentuales
         */
        List<InstantEffectPermanentDefinition> permEffectsPercentageValue = permanentEffects.Where(e => e.StatAffected.Equals(statName) && e.IsValueInPercentage).ToList();
        foreach (InstantEffectPermanentDefinition permanentEffect in permEffectsPercentageValue)
        {
            //TODO Mejora: Poder incrementar porcentualmente en función de otros stats (StatWhatToSee)
            permanentEffect.CalculateRealPercentage();
            //percentualValue += permanentEffect.GetRealValue();
            ChangeStat(permanentEffect);
        }

        /*
         * 4º Se calculan los cambios temporales porcentuales
         */
        List<InstantEffectTemporallyDefinition> tempEffectsPercentValue = temporallyEffects.Where(e => e.StatAffected.Equals(statName) && e.IsValueInPercentage).ToList();
        foreach (InstantEffectTemporallyDefinition temporallyEffect in tempEffectsPercentValue)
        {
            temporallyEffect.CalculateRealPercentage();
            ChangeStat(temporallyEffect);
        }

        /*
         * 5º Se calculan los Duringtimeffect cambios temporales
         */
        foreach (DuringTimeEffectDefinition duringTimeEffect in duringTimeEffects)
        {
            if (duringTimeEffect.IsValueInPercentage)
            {
                duringTimeEffect.CalculateRealPercentage();
            }
            ChangeStat(duringTimeEffect);
        }

        /*
         * 7º Se calculan los Overtimeffect cambios temporales
         */
        /*foreach (OverTimeEffectDefinition overTimeEffect in overTimeEffects)
        {
            if (overTimeEffect.IsValueInPercentage)
            {
                overTimeEffect.CalculateRealPercentage();
            }
            if(overTimeEffect.timesApplied > 0)
            {
                ChangeStat(overTimeEffect, overTimeEffect.timesApplied);
            }
        }*/
    }

    #region StatChanges
    public void ChangeStat(EffectDefinition effect, int timesApplied = 0)
    {
        Stat stat = GetStatFromName(effect.StatAffected);
        int value = effect.Value;
        /*if(timesApplied > 0)
        {
            value = effect.Value * timesApplied;
        }*/
        switch (effect.StatPart)
        {
            case StatParts.ACTUAL_MAX_VALUE:
                if (effect.IsStatIncremented)
                {
                    stat.IncreaseActualMaxValue(value);
                }
                else
                {
                    stat.DecreaseActualMaxValue(value);
                }
                break;
            case StatParts.ACTUAL_VALUE:
                if (effect.IsStatIncremented)
                {
                    stat.IncreaseValue(value);
                }
                else
                {
                    stat.DecreaseValue(value);
                }
                break;
        }
    }



    //public void ChangeActualStat(StatNames statName, int value)
    public void ChangeActualStat(EffectDefinition effect)
    {
        /**
         * Hacer esto en el process effect!!!!!
         * Si se va a añadir valor realizar las comprobaciones pertinentes en algunos casos.
         * Si se va a quitar valor (es decir, valor negativo), se busca
         * un efecto con esas características en la lista que corresponda.
         * Si existe -> Quitar efecto y recalcular valor
         * Si no existe -> Añadir efecto y recalcular valor
         */
        Stat stat = GetStatFromName(effect.StatAffected);
        if (effect.Value < 0)
        {
            stat.DecreaseValue(effect.Value);
        }
        else
        {
            Debug.Log("TODO Check effects types. Can't be 2 of the same type. Example only can have one poisson effect active. Do this in the processEffect!!");
            stat.IncreaseValue(effect.Value);
        }
    }

    public void ChangeActualStatInPercentage(EffectDefinition effect)
    {
        if (effect.IsStatIncremented)
        {
            GetStatFromName(effect.StatAffected).IncreaseValueInPercentage(effect.Value);
        }
        else
        {
            GetStatFromName(effect.StatAffected).DecreaseValueInPercentage(effect.Value);
        }
    }

    public void ChangeActualMaxStat(EffectDefinition effect)
    {
        Stat stat = GetStatFromName(effect.StatAffected);
        if (effect.Value < 0)
        {
            stat.DecreaseActualMaxValue(effect.Value);
        }
        else
        {
            stat.IncreaseActualMaxValue(effect.Value);
        }
    }

    #endregion StatChanges

    public bool ApplyDamage(Strike strike)
    {
        bool isAlive = true;

        Stat stat = GetStatFromName(strike.Effect.StatAffected);
        stat.DecreaseValue(strike.FinalValue);

        if (stat.Value <= 0)
        {
            isAlive = false;
        }

        return isAlive;
    }

    public bool ApplyHeal()
    {
        Debug.Log("TODO");
        return false;
    }


    public DuringTimeEffectDefinition GetDuringTimeEffectByType(EffectTypesEnum effectType)
    {
        DuringTimeEffectDefinition duringTimeEffect = duringTimeEffects.Where(e => e.EffectType.Equals(effectType)).FirstOrDefault();

        return duringTimeEffect;
    }
    public OverTimeEffectDefinition GetOverTimeEffectByType(EffectTypesEnum effectType)
    {
        OverTimeEffectDefinition overTimeEffect = overTimeEffects.Where(e => e.EffectType.Equals(effectType)).FirstOrDefault();

        return overTimeEffect;
    }


    public bool TryAddEffect(EffectDefinition effect)
    {
        bool hasBeenAdded = false;
        if (effect is OverTimeEffectDefinition)
        {
            overTimeEffects.Add(effect as OverTimeEffectDefinition);
            hasBeenAdded = true;
        }
        else if (effect is DuringTimeEffectDefinition)
        {
            duringTimeEffects.Add(effect as DuringTimeEffectDefinition);
            hasBeenAdded = true;

        }
        else if (effect is InstantEffectPermanentDefinition)
        {
            permanentEffects.Add(effect as InstantEffectPermanentDefinition);
            hasBeenAdded = true;
        }
        else if (effect is InstantEffectTemporallyDefinition)
        {
            temporallyEffects.Add(effect as InstantEffectTemporallyDefinition);
            hasBeenAdded = true;
        }

        CalculateStat(effect.StatAffected, effect.StatPart);
        return hasBeenAdded;
    }

    public bool TryRemoveEffect(EffectDefinition effect)
    {
        bool hasBeenRemoved = false;
        if (effect is OverTimeEffectDefinition)
        {
            Debug.Log("TODO remove OverTimeEffectDefinition");
        }
        else if (effect is DuringTimeEffectDefinition)
        {
            int previousLength = duringTimeEffects.Count;

            DuringTimeEffectDefinition duringTimeEffect = GetDuringTimeEffectByType(effect.EffectType);
            duringTimeEffects.Remove(duringTimeEffect);

            int afterRemoveLength = duringTimeEffects.Count;

            if (previousLength == afterRemoveLength + 1)
            {
                Debug.Log("Effect has been removed");
                hasBeenRemoved = true;
            }
        }
        else if (effect is InstantEffectPermanentDefinition)
        {
            Debug.Log("TODO remove InstantEffectPermanentDefinition");
        }
        else if (effect is InstantEffectTemporallyDefinition)
        {
            int previousLength = temporallyEffects.Count;

            InstantEffectTemporallyDefinition effecto = temporallyEffects.Where(e => e.Equals(effect)).FirstOrDefault();
            temporallyEffects.Remove(effecto);

            int afterRemoveLength = temporallyEffects.Count;

            if (previousLength == afterRemoveLength + 1)
            {
                Debug.Log("Effect has been removed");
                hasBeenRemoved = true;
            }
        }

        CalculateStat(effect.StatAffected, effect.StatPart);
        return hasBeenRemoved;
    }
}

    
    
    //Se le pasa el valor m�ximo del stat?
    //Creo una funci�n recursiva?
    /*private int CalculateStat(StatsEnum stat)
    {
        float value1 = GetStatValueFromList(baseStats, stat) + GetStatValueFromList(instantStatsModifyPermanent, stat);
        float value2 = GetStatValueFromList(instantStatsModifyInRun, stat);
        float value3 = 1 + (GetStatValueFromList(instantStatsModifyPercentageInRun, stat) * 0.01f);
        float valueCalculated = (value1 + value2) * value3;

        valueCalculated = Mathf.Clamp(valueCalculated, baseStats.Find(t => t.Name.Equals(stat)).MinValue, GetActualMaxStatValue(stat));

        //return Math.Clamp(valueCalculated, 0, MaxStatsValues.GetStat(stat));
        return (int)valueCalculated;
    }*/

    /*private int GetStatValueFromList(List<Stat> list, StatsEnum stat)
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

    //Servir� para feedback en la UI sobre todo
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
    }*/

    /**
     * TODO refactor
     * 
     * Para poder incrementar cualquier maxValue
     * Tambi�n habr�a que a�adir un nuevo check en los efectos que haga referencia
     * a los max values. As� el statusEnum dejar�a de tener MAX_HEALTH y no
     * habr�a que hardcodear aqu� el statEnum Health
     */
    /*public void PerformApplyStatModifyInRun(StatModificator statModificator)
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

    public StatModificator PerformRealHealthChange(StatModificator statModificator)
    {
        if(statModificator.Value > 0)
        {
            statModificator = Heal(statModificator);
        }
        else
        {
            if (statModificator.BuffDebuffType.Equals(EffectTypes.POISON))
            {
                statModificator = TakeRealDamage(statModificator);
            }
            else if (statModificator.IsAttack)
            {
                statModificator = TakeDamage(statModificator);
            }
        }

        return statModificator;
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
    }*/


    /**
     * TODO ?
     * Si el TakeDamage fuese diferente en el personaje y en los enemigos
     * debe implementarse en cada uno por separado
     */
    /*public StatModificator TakeDamage(StatModificator statModificator)
    {
        
        int defense = GetStatValue(StatsEnum.DEFENSE);
        int realDamage = Math.Abs(statModificator.Value) - defense;
        int finalDamage = realDamage < 0 ? 0 : realDamage;

        statModificator.Value = -finalDamage;

        PerformApplyStatModifyInRun(statModificator);

        statModificator.IsAlive = CheckIsAlive();

        return statModificator;
    }

    public StatModificator TakeRealDamage(StatModificator statModificator)
    {
        PerformApplyStatModifyInRun(statModificator);
        statModificator.IsAlive = CheckIsAlive();

        return statModificator;
    }

    public StatModificator Heal(StatModificator statModificator)
    {
        int maxHealth = GetActualBaseMaxStat(statModificator.StatToModify);
        int health = GetStatValue(statModificator.StatToModify);
        int finalHeal = Math.Clamp(statModificator.Value, 0, maxHealth - health);

        statModificator.Value = finalHeal;
        PerformApplyStatModifyInRun(statModificator);

        return statModificator;
    }*/

    /*private bool CheckIsAlive()
    {

        bool isAlive = GetStatValue(StatsEnum.HEALTH) > 0;
        
        if(!isAlive)
        {
            Debug.Log("DIE!");
        }
        
        return isAlive;
    }*/



