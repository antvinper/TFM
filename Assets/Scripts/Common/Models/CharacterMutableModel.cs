using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using System.Linq;

[Serializable]
public class CharacterMutableModel : ICharacterModel, ICharacterModelStats
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float health = 100;
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

    [SerializeField] private StatsIncrement statsIncrementPermanent;
    [SerializeField] private StatsIncrement statsIncrementTemporally;

    [JsonIgnore] private List<TimeEffectDefinition> timeEffectDefinitions = new List<TimeEffectDefinition>();

    [JsonIgnore]
    [HideInInspector]
    public List<TimeEffectDefinition> TimeEffectDefinitions { get => timeEffectDefinitions; }

    [JsonProperty]
    public float MaxHealth { 
        get => maxHealth + statsIncrementPermanent.MaxHealth + statsIncrementTemporally.MaxHealth; 
        set => maxHealth = value; 
    }
    [JsonIgnore]
    public float Health { 
        get => health + statsIncrementTemporally.Health; 
        set 
        {
            health = value; 
        }
    }
    [JsonProperty]
    public int Defense { 
        get => defense + statsIncrementPermanent.Defense + statsIncrementTemporally.Defense; 
        set => defense = value; 
    }
    [JsonProperty]
    public int MagicDefense { 
        get => magicDefense + statsIncrementPermanent.MagicDefense + statsIncrementTemporally.MagicDefense; 
        set => magicDefense = value; 
    }
    [JsonProperty]
    public int Attack { 
        get => attack + statsIncrementPermanent.Attack + statsIncrementTemporally.Attack; 
        set => attack = value; 
    }
    [JsonProperty]
    public int MagicAttack { 
        get => magicAttack + statsIncrementPermanent.MagicAttack + statsIncrementTemporally.MagicAttack;
        set => magicAttack = value;
    }
    [JsonProperty]
    public int Speed { 
        get => speed + statsIncrementPermanent.Speed + statsIncrementTemporally.Speed;
        set => speed = value;
    }
    [JsonProperty]
    public int Shield { 
        get => shield + statsIncrementPermanent.Shield + statsIncrementTemporally.Shield; 
        set => shield = value; 
    }
    [JsonProperty]
    public int Accuracy { 
        get => accuracy + statsIncrementPermanent.Accuracy + statsIncrementTemporally.Accuracy; 
        set => accuracy = value; 
    }
    [JsonProperty]
    public int BlockChance { 
        get => blockChance + statsIncrementPermanent.BlockChance + statsIncrementTemporally.BlockChance; 
        set => blockChance = value; 
    }
    public int DodgeChance { 
        get => dodgeChance + statsIncrementPermanent.DodgeChance + statsIncrementTemporally.DodgeChance;
        set => dodgeChance = value;
    }
    [JsonProperty]
    public int CritChance { 
        get => critChance + statsIncrementPermanent.CritChance + statsIncrementTemporally.CritChance; 
        set => critChance = value; 
    }
    [JsonProperty]
    public int CritDamageMultiplier { 
        get => critDamageMultiplier + statsIncrementPermanent.CritDamageMultiplier + statsIncrementTemporally.CritDamageMultiplier; 
        set => critDamageMultiplier = value; 
    }

    [JsonProperty]
    public StatsIncrement StatsIncrement
    {
        get => statsIncrementPermanent;
        set => statsIncrementPermanent = value;
    }

    [JsonIgnore]
    public StatsIncrement StatsIncrementInRun
    {
        get => statsIncrementTemporally;
        set => statsIncrementTemporally = value;
    }


    public void PerformPermanentChangeState(StatModificator statModificator)
    {
        statsIncrementPermanent.ChangeStat(statModificator.StatToModify, statModificator.Value);
        
    }

    public void PerformTemporallyState(StatModificator statModificator)
    {
        statsIncrementTemporally.ChangeStat(statModificator.StatToModify, statModificator.Value);
    }

    public void PerformRealHealthChange(StatModificator statModificator)
    {
        if (statModificator.BuffDebuffType.Equals(BuffDebuffTypes.POISON))
        {
            TakeRealDamage(statModificator);
        }
        else if(statModificator.IsAttack)
        {
            TakeDamage(statModificator);
        }
    }
     public void PerformPercentualHealthChange(StatModificator statModificator)
    {
        if(statModificator.IsAttack)
        {
            TakeRealDamage(statModificator);
        }
    }


    /**
     * TODO ?
     * Si el TakeDamage fuese diferente en el personaje y en los enemigos
     * debe implementarse en cada uno por separado
     */
    public void TakeDamage(StatModificator statModificator)
    {
        float realDamage = Defense + statModificator.Value;
        float finalDamage = realDamage < 0 ? 0 : realDamage;

        statsIncrementTemporally.ChangeStat(statModificator.StatToModify, finalDamage);

        statModificator.IsAlive = CheckIsAlive();
    }

    public void TakeRealDamage(StatModificator statModificator)
    {
        statsIncrementTemporally.ChangeStat(statModificator.StatToModify, statModificator.Value);
        statModificator.IsAlive = CheckIsAlive();
    }

    public void Heal(StatModificator statModificator)
    {
        float finalHeal = Math.Clamp(statModificator.Value + Health, 0, maxHealth - Health);

        statsIncrementTemporally.ChangeStat(statModificator.StatToModify, finalHeal);
    }

    private bool CheckIsAlive()
    {
        /**
         * TODO
         * Debería llamar a algún evento de muerte??
         */
        return Health > 0;
    }

}
