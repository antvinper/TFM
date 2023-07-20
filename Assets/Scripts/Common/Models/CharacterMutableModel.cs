using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using System.Linq;

[Serializable]
public class CharacterMutableModel : ICharacterModel, ICharacterModelStats
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int health = 100;
    [SerializeField] private int defense = 10;
    [SerializeField] private int magicDefense = 10;
    [SerializeField] private int attack = 10;
    [SerializeField] private int magicAttack = 10;
    [SerializeField] private int speed = 10;
    [SerializeField] private int shield = 10;
    [SerializeField] [Range(0, 100)] private int accuracy = 100;
    [SerializeField] [Range(0, 100)] private int blockChance = 0;
    [SerializeField] [Range(0, 100)] private int dodgeChance = 0;
    [SerializeField] [Range(0, 100)] private int critChance = 0;
    [SerializeField] [Range(0, 100)] private int critDamageMultiplier = 0;

#region statsModifiers
    private StatsModifyInstantPermanently InstantStatsModifyPermanent = new StatsModifyInstantPermanently();

    //Se mantienen durante la run
    private StatsModifyInstantTemporally InstantStatsModifyInRun = new StatsModifyInstantTemporally();
    private StatsModifyInstantTemporallyInPercentage InstantStatsModifyPercentageInRun = new StatsModifyInstantTemporallyInPercentage();

    //Terminan tras un tiempo
    private StatsModifyTime timeStatsModify = new StatsModifyTime();
    private StatsModifyTimeInPercentage timeStatsModifyPercentage = new StatsModifyTimeInPercentage();

#endregion statsModifiers

    [JsonIgnore] private List<TimeEffectDefinition> timeEffectDefinitions = new List<TimeEffectDefinition>();

    [JsonIgnore]
    [HideInInspector]
    public List<TimeEffectDefinition> TimeEffectDefinitions { get => timeEffectDefinitions; }


    private float CalculateStat(StatsEnum stat, int baseStat)
    {
        float value1 = baseStat + InstantStatsModifyPermanent.GetStatValue(stat);
        float value2 = InstantStatsModifyInRun.GetStatValue(stat) + timeStatsModify.GetStatValue(stat);
        float value3 = InstantStatsModifyPercentageInRun.GetStatValue(stat) + timeStatsModifyPercentage.GetStatValue(stat);

        float valueCalculated = (value1 + value2) * (1 + (value3 * 0.01f));

        return Math.Clamp(valueCalculated, 0, MaxStatsValues.GetStat(stat));
    }

    [JsonProperty]
    public int MaxHealth {
        get
        {
            return (int)CalculateStat(StatsEnum.MAX_HEALTH, maxHealth);
        }
        //get => maxHealth + statsModifyPermanent.MaxHealth + statsModifyTemporally.MaxHealth;
        set => maxHealth = value;
    }

    [JsonIgnore]
    public int Health {
        get
        {
            return (int)CalculateStat(StatsEnum.HEALTH, health);
        }
        //get => health + statsModifyTemporally.Health; 
        set 
        {
            health = value; 
        }
    }
    [JsonProperty]
    public int Defense {
        get
        {
            return (int)CalculateStat(StatsEnum.DEFENSE, defense);
        }
        //get => defense + statsModifyPermanent.Defense + statsModifyTemporally.Defense; 
        set => defense = value; 
    }
    [JsonProperty]
    public int MagicDefense {
        get
        {
            return (int)CalculateStat(StatsEnum.MAGIC_DEFENSE, magicDefense);
        }
        //get => magicDefense + statsModifyPermanent.MagicDefense + statsModifyTemporally.MagicDefense; 
        set => magicDefense = value; 
    }
    [JsonProperty]
    public int Attack {
        get
        {
            return (int)CalculateStat(StatsEnum.ATTACK, attack);
        }
        //get => attack + statsModifyPermanent.Attack + statsModifyTemporally.Attack; 
        set => attack = value; 
    }
    [JsonProperty]
    public int MagicAttack {
        get
        {
            return (int)CalculateStat(StatsEnum.MAGIC_ATTACK, magicAttack);
        }
       // get => magicAttack + statsModifyPermanent.MagicAttack + statsModifyTemporally.MagicAttack;
        set => magicAttack = value;
    }
    [JsonProperty]
    public int Speed {
        get
        {
            return (int)CalculateStat(StatsEnum.SPEED, speed);
        }
        //get => speed + statsModifyPermanent.Speed + statsModifyTemporally.Speed;
        set => speed = value;
    }
    [JsonProperty]
    public int Shield {
        get
        {
            return (int)CalculateStat(StatsEnum.SHIELD, shield);
        }
        //get => shield + statsModifyPermanent.Shield + statsModifyTemporally.Shield; 
        set => shield = value; 
    }
    [JsonProperty]
    public int Accuracy {
        get
        {
            return (int)CalculateStat(StatsEnum.ACCURACY, accuracy);
        }
        //get => accuracy + statsModifyPermanent.Accuracy + statsModifyTemporally.Accuracy; 
        set => accuracy = value; 
    }
    [JsonProperty]
    public int BlockChance {
        get
        {
            return (int)CalculateStat(StatsEnum.BLOCK_CHANCE, blockChance);
        }
        //get => blockChance + statsModifyPermanent.BlockChance + statsModifyTemporally.BlockChance; 
        set => blockChance = value; 
    }
    public int DodgeChance {
        get
        {
            return (int)CalculateStat(StatsEnum.DODGE_CHANCE, dodgeChance);
        }
        //get => dodgeChance + statsModifyPermanent.DodgeChance + statsModifyTemporally.DodgeChance;
        set => dodgeChance = value;
    }
    [JsonProperty]
    public int CritChance {
        get
        {
            return (int)CalculateStat(StatsEnum.CRIT_CHANCE, critChance);
        }
        //get => critChance + statsModifyPermanent.CritChance + statsModifyTemporally.CritChance; 
        set => critChance = value; 
    }
    [JsonProperty]
    public int CritDamageMultiplier {
        get
        {
            return (int)CalculateStat(StatsEnum.CRIT_DAMAGE_MULTIPLIER, critDamageMultiplier);
        }
        //get => critDamageMultiplier + statsModifyPermanent.CritDamageMultiplier + statsModifyTemporally.CritDamageMultiplier; 
        set => critDamageMultiplier = value; 
    }

    [JsonProperty]
    public StatsModifyInstantPermanently StatsIncrement
    {
        get => InstantStatsModifyPermanent;
        set => InstantStatsModifyPermanent = value;
    }
    [JsonIgnore]
    public StatsModifyInstantTemporally StatsModifyInRun
    {
        get => InstantStatsModifyInRun;
        set => InstantStatsModifyInRun = value;
    }

    [JsonIgnore]
    public StatsModifyInstantTemporallyInPercentage StatsModifyPercentageInRun
    {
        get => InstantStatsModifyPercentageInRun;
        set => InstantStatsModifyPercentageInRun = value;
    }

    [JsonIgnore]
    public StatsModifyTime StatsIncrementInRun
    {
        get => timeStatsModify;
        set => timeStatsModify = value;
    }
    [JsonIgnore]
    public StatsModifyTimeInPercentage StatsModifyPercentageTemporally
    {
        get => timeStatsModifyPercentage;
        set => timeStatsModifyPercentage = value;
    }

    /**
     * TODO 
     * Do it with the rest of the stats
     */
    /*[JsonIgnore]
    public int MaxHealthWithPermanent
    {
        get => maxHealth + InstantStatsModifyPermanent.MaxHealth;
    }
    [JsonIgnore]
    public int SpeedWithPermanent
    {
        get => speed + InstantStatsModifyPermanent.Speed;
    }*/

    public void PerformApplyPermanentStat(StatModificator statModificator)
    {
        InstantStatsModifyPermanent.ApplyStat(statModificator);
    }

    public void PerformApplyStatModifyInRun(StatModificator statModificator)
    {
        if (statModificator.IsPercentual)
        {
            InstantStatsModifyPercentageInRun.ApplyStat(statModificator);
        }
        else
        {
            InstantStatsModifyInRun.ApplyStat(statModificator);
        }
    }

    public void PerformApplyTimeStatModifyInRun(StatModificator statModificator)
    {
        if (statModificator.IsPercentual)
        {
            timeStatsModifyPercentage.ApplyStat(statModificator);
        }
        else
        {
            timeStatsModify.ApplyStat(statModificator);
        }
    }




    public void PerformInstantlyApplyStat(StatModificator statModificator)
    {
        if (GameManager.Instance.IsGameInRun)
        {
            //TODO
            InstantStatsModifyInRun.ApplyStat(statModificator);
        }
        else
        {
            InstantStatsModifyPermanent.ApplyStat(statModificator);
            //statsIncrementPermanent.ChangeStat(statModificator.StatToModify, statModificator.Value);
        }
        
        
    }

    public void PerformTemporallyStat(StatModificator statModificator)
    {
        timeStatsModify.ApplyStat(statModificator);
        //statsIncrementTemporally.ChangeStat(statModificator.StatToModify, statModificator.Value);
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
        //float realDamage = Defense + statModificator.Value;
        int realDamage = Math.Abs(statModificator.Value) - Defense;
        int finalDamage = realDamage < 0 ? 0 : realDamage;

        statModificator.Value = -finalDamage;

        InstantStatsModifyInRun.ApplyStat(statModificator);
        //statsIncrementTemporally.ChangeStat(statModificator.StatToModify, -finalDamage);

        statModificator.IsAlive = CheckIsAlive();
    }

    public void TakeRealDamage(StatModificator statModificator)
    {
        InstantStatsModifyInRun.ApplyStat(statModificator);
        //statsIncrementTemporally.ChangeStat(statModificator.StatToModify, statModificator.Value);
        statModificator.IsAlive = CheckIsAlive();
    }

    public void Heal(StatModificator statModificator)
    {
        int finalHeal = Math.Clamp(statModificator.Value, 0, maxHealth - Health);

        statModificator.Value = finalHeal;
        InstantStatsModifyInRun.ApplyStat(statModificator);
        //statsIncrementTemporally.ChangeStat(statModificator.StatToModify, finalHeal);
    }

    private bool CheckIsAlive()
    {
        /**
         * TODO
         * Debería llamar a algún evento de muerte??
         */
        return Health > 0;
    }

    public int GetStat(StatsEnum stat)
    {
        int value = 0;

        switch (stat)
        {
            case StatsEnum.MAX_HEALTH:
                value = MaxHealth;
                break;
            case StatsEnum.HEALTH:
                value = Health;
                break;
            case StatsEnum.DEFENSE:
                value = Defense;
                break;
            case StatsEnum.MAGIC_DEFENSE:
                value = MagicDefense;
                break;
            case StatsEnum.ATTACK:
                value = Attack;
                break;
            case StatsEnum.MAGIC_ATTACK:
                value = MagicAttack;
                break;
            case StatsEnum.SPEED:
                value = Speed;
                break;
            case StatsEnum.SHIELD:
                value = Shield;
                break;
            case StatsEnum.ACCURACY:
                value = Accuracy;
                break;
            case StatsEnum.BLOCK_CHANCE:
                value = BlockChance;
                break;
            case StatsEnum.DODGE_CHANCE:
                value = DodgeChance;
                break;
            case StatsEnum.CRIT_CHANCE:
                value = CritChance;
                break;
            case StatsEnum.CRIT_DAMAGE_MULTIPLIER:
                value = CritDamageMultiplier;
                break;
        }

        return value;
    }

}
