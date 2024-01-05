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

    
    
    protected List<TreeForModelStruct> treeForModelStructs;

    [JsonIgnore] protected List<StatModificationPermanent> statsModificationPermanent;

    [JsonIgnore] protected List<OverTimeEffectDefinition> overTimeEffects;
    [JsonIgnore] protected List<DuringTimeEffectDefinition> duringTimeEffects;
    [JsonIgnore] protected List<InstantEffectTemporallyDefinition> temporallyEffects;

    [JsonIgnore] public List<OverTimeEffectDefinition> OverTimeEffects { get => overTimeEffects; }
    [JsonIgnore] public List<DuringTimeEffectDefinition> DuringTimeEffects { get => duringTimeEffects; }
    [JsonIgnore] public List<InstantEffectTemporallyDefinition> TemporallyEffects { get => temporallyEffects; }

    
    [JsonProperty]
    public List<StatModificationPermanent> StatsModificationPermanent
    {
        get => statsModificationPermanent;
        set => statsModificationPermanent = value;
    }
     
    public CharacterMutableModel() { }

    public virtual void Setup(CharacterStatsDefinition characterStatsDefinition)
    {
        this.characterStatsDefinition = characterStatsDefinition;
        stats = new List<Stat>();
        overTimeEffects = new List<OverTimeEffectDefinition>();
        duringTimeEffects = new List<DuringTimeEffectDefinition>();
        if(statsModificationPermanent == null)
        {
            statsModificationPermanent = new List<StatModificationPermanent>();
        }
        
        temporallyEffects = new List<InstantEffectTemporallyDefinition>();

        foreach(StatDefinition sd in characterStatsDefinition.statsDefinition.stats)
        {
            Stat stat = new Stat(sd.name, sd.value, sd.actualMaxValue, sd.maxValue, sd.minValue, sd.isVolatil);
            stats.Add(stat);
        }
        CalculateStats();
    }

    protected void CalculateStats()
    {
        foreach(Stat stat in stats)
        {
            CalculateStat(stat.StatName, StatParts.MIN_VALUE);
            CalculateStat(stat.StatName, StatParts.MAX_VALUE);
            CalculateStat(stat.StatName, StatParts.ACTUAL_MAX_VALUE);
            CalculateStat(stat.StatName, StatParts.ACTUAL_VALUE);
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

    public virtual void CalculateStat(StatNames statName, StatParts statPart)
    {
        GetStatFromName(statName).ResetStat();


        /**
         * 1º Calcular valor añadido no porcentual permanente y temporal
         * 2º Calcular valor añadido no porcentual de los time effects
         * 3º Calcular valor añadido porcentual (en porcentaje) permanente y temporal
         * 4º Calcular valor añadido porcentual de los time effects
         */


        /*
         * 1º se calculan los cambios permanentes no porcentuales
         */
        CalculateNotPercentualPermanents(statName);


        /*
         * 2º Se calculan los cambios temporales no porcentuales
         */
        CalculateNotPercentualTemporally(statName);

        /*
         * 3º se calculan los cambios permanentes porcentuales
         */
        CalculatePercentualPermanents(statName);

        /*
         * 4º Se calculan los cambios temporales porcentuales
         */
        CalculatePercentualTemporally(statName);

        /*
         * 5º Se calculan los Duringtimeffect cambios temporales
         */
        CalculateDuringTimeEffects(statName);

        /* TODO
         * 7º Se calculan los Overtimeffect cambios temporales
         * Conviene usarlo para, por ejemplo, reducir la velocidad de golpe 10 puntos,
         * e ir incrementándola poco a poco hasta volver a la original. Esto se haría
         * combinando un instant effect temporally que reduzca 10 puntos
         * junto con un overtimeEffect que vaya aumentando cada x tiempo y puntos.
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

    protected void CalculateDuringTimeEffects(StatNames statName)
    {
        List<DuringTimeEffectDefinition> duringTimeEffectsFromStat = duringTimeEffects.Where(e => e.StatAffected.Equals(statName)).ToList();
        foreach (DuringTimeEffectDefinition duringTimeEffect in duringTimeEffectsFromStat)
        {
            if (duringTimeEffect.IsValueInPercentage)
            {
                duringTimeEffect.CalculateRealPercentage();
            }
            ChangeStat(duringTimeEffect);
        }
    }

    protected void CalculatePercentualTemporally(StatNames statName)
    {
        List<InstantEffectTemporallyDefinition> tempEffectsPercentValue = temporallyEffects.Where(e => e.StatAffected.Equals(statName) && e.IsValueInPercentage).ToList();
        foreach (InstantEffectTemporallyDefinition temporallyEffect in tempEffectsPercentValue)
        {
            temporallyEffect.CalculateRealPercentage();
            ChangeStat(temporallyEffect);
        }
    }


    protected void CalculatePercentualPermanents(StatNames statName)
    {
        List<StatModificationPermanent> permEffectsPercentageValue = statsModificationPermanent.Where(e => e.StatAffected.Equals(statName) && e.IsValueInPercentage).ToList();
        foreach (StatModificationPermanent permanentEffect in permEffectsPercentageValue)
        {
            //TODO Mejora: Poder incrementar porcentualmente en función de otros stats (StatWhatToSee)
            permanentEffect.CalculateRealPercentage(GetStatValue(permanentEffect.StatWhatToSee, permanentEffect.StatWhatToSeeStatPart));
            ChangeStat(permanentEffect);
        }
    }

    protected void CalculateNotPercentualTemporally(StatNames statName)
    {
        List<InstantEffectTemporallyDefinition> tempEffectsRealValue = temporallyEffects.Where(e => e.StatAffected.Equals(statName) && !e.IsValueInPercentage).ToList();
        foreach (InstantEffectTemporallyDefinition temporallyEffect in tempEffectsRealValue)
        {
            ChangeStat(temporallyEffect);
        }
    }


    protected void CalculateNotPercentualPermanents(StatNames statName)
    {
        List<StatModificationPermanent> statsModificationPermanentRealValue = statsModificationPermanent.Where(e => e.StatAffected.Equals(statName) && !e.IsValueInPercentage).ToList();
        foreach (StatModificationPermanent statModificationPermanentRealValue in statsModificationPermanentRealValue)
        {
            statModificationPermanentRealValue.GetRealValue();
            ChangeStat(statModificationPermanentRealValue);
        }
    }

    public void ChangeStat(StatModificationPermanent statModification)
    {
        Stat stat = GetStatFromName(statModification.StatAffected);
        int value = statModification.Value;
        switch (statModification.StatPart)
        {
            case StatParts.ACTUAL_MAX_VALUE:
                if (statModification.IsStatIncremented)
                {
                    stat.IncreaseActualMaxValue(value);
                }
                else
                {
                    stat.DecreaseActualMaxValue(value);
                }
                break;
            case StatParts.ACTUAL_VALUE:
                if (statModification.IsStatIncremented)
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

    #region StatChanges
    public void ChangeStat(EffectDefinition effect, int timesApplied = 0)
    {
        Stat stat = GetStatFromName(effect.StatAffected);
        int value = effect.Value;
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

    public void ChangeActualStat(EffectDefinition effect)
    {
        /**
         * Hacer esto en el process effect!!!!!??????
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

    public virtual bool TryAddEffect(EffectDefinition effect, bool isFromTree = false, int index = -1)
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
            StatModificationPermanent statModificationPermanent = new StatModificationPermanent(effect as InstantEffectPermanentDefinition);
            if (isFromTree)
            {
                TreeForModelStruct treeStruct = treeForModelStructs.Where(t => t.index.Equals(index)).FirstOrDefault();
                treeStruct.isActive = true;
            }
            else
            {
                statsModificationPermanent.Add(statModificationPermanent);
            }
            hasBeenAdded = true;
        }
        else if (effect is InstantEffectTemporallyDefinition)
        {
            temporallyEffects.Add(effect as InstantEffectTemporallyDefinition);
            hasBeenAdded = true;
        }

        CalculateStats();
        return hasBeenAdded;
    }

    public bool TryRemoveEffect(EffectDefinition effect)
    {
        bool hasBeenRemoved = false;
        if (effect is OverTimeEffectDefinition)
        {
            int previousLength = overTimeEffects.Count;
            OverTimeEffectDefinition overTimeEffect = GetOverTimeEffectByType(effect.EffectType);
            overTimeEffects.Remove(overTimeEffect);
            int afterRemoveLength = overTimeEffects.Count;

            if (previousLength == afterRemoveLength + 1)
            {
                Debug.Log("Effect has been removed");
                hasBeenRemoved = true;
            }
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
            Debug.Log("TODO remove InstantEffectPermanentDefinition from tree");
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

        CalculateStats();
        return hasBeenRemoved;
    }
}