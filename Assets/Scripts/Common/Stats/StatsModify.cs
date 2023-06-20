using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]
public class StatsModify : ICharacterModelStats
{
    [SerializeField] protected int maxHealth = 0;
    //Es necesaria sólo para calcular la vida que tiene
    protected int health = 0;
    [SerializeField] protected int defense = 0;
    [SerializeField] protected int magicDefense = 0;
    [SerializeField] protected int attack = 0;
    [SerializeField] protected int magicAttack = 0;
    [SerializeField] protected int speed = 0;
    [SerializeField] protected int shield = 0;
    [SerializeField] [Range(0, 100)] protected int accuracy = 0;
    [SerializeField] [Range(0, 100)] protected int blockChance = 0;
    [SerializeField] [Range(0, 100)] protected int dodgeChance = 0;
    [SerializeField] [Range(0, 100)] protected int critChance = 0;
    [SerializeField] [Range(0, 100)] protected int critDamageMultiplier = 0;

    protected List<StatModificator> stats = new List<StatModificator>();

    public virtual void ApplyStat(StatModificator statModificator)
    {
        stats.Add(statModificator);
    }

    public virtual void RemoveStat(StatModificator statModificator)
    {

    }
    
    protected void RecalculateStats()
    {

    }

    [JsonProperty]
    public int MaxHealth
    {
        get => maxHealth;
        set => maxHealth = value;
    }
    //No es necesaria, pero se necesita para usar el ICharacterModelStats
    [JsonIgnore]
    public int Health
    {
        get => health;
        set => health = value;
    }
    [JsonProperty]
    public int Defense
    {
        get => defense;
        set => defense = value;
    }
    [JsonProperty]
    public int MagicDefense
    {
        get => magicDefense;
        set => magicDefense = value;
    }
    [JsonProperty]
    public int Attack
    {
        get => attack;
        set => attack = value;
    }
    [JsonProperty]
    public int MagicAttack
    {
        get => magicAttack;
        set => magicAttack = value;
    }
    [JsonProperty]
    public int Speed
    {
        get => speed;
        set => speed = value;
    }
    [JsonProperty]
    public int Shield
    {
        get => shield;
        set => shield = value;
    }
    [JsonProperty]
    public int Accuracy
    {
        get => accuracy;
        set => accuracy = value;
    }
    [JsonProperty]
    public int BlockChance
    {
        get => blockChance;
        set => blockChance = value;
    }
    public int DodgeChance
    {
        get => dodgeChance;
        set => dodgeChance = value;
    }
    [JsonProperty]
    public int CritChance
    {
        get => critChance;
        set => critChance = value;
    }
    [JsonProperty]
    public int CritDamageMultiplier
    {
        get => critDamageMultiplier;
        set => critDamageMultiplier = value;
    }

    public int GetStatValue(StatsEnum stat)
    {
        int statValue = 0;
        switch (stat)
        {
            case StatsEnum.MAX_HEALTH:
                statValue = maxHealth;
                break;
            case StatsEnum.HEALTH:
                statValue = health;
                break;
            case StatsEnum.DEFENSE:
                statValue = defense;
                break;
            case StatsEnum.MAGIC_DEFENSE:
                statValue = magicDefense;
                break;
            case StatsEnum.ATTACK:
                statValue = attack;
                break;
            case StatsEnum.MAGIC_ATTACK:
                statValue = magicAttack;
                break;
            case StatsEnum.SPEED:
                statValue = speed;
                break;
            case StatsEnum.SHIELD:
                statValue = shield;
                break;
            case StatsEnum.ACCURACY:
                statValue = accuracy;
                break;
            case StatsEnum.BLOCK_CHANCE:
                statValue = blockChance;
                break;
            case StatsEnum.DODGE_CHANCE:
                statValue = dodgeChance;
                break;
            case StatsEnum.CRIT_CHANCE:
                statValue = critChance;
                break;
            case StatsEnum.CRIT_DAMAGE_MULTIPLIER:
                statValue = critDamageMultiplier;
                break;
        }

        return statValue;
    }

    protected void ChangeStat(StatsEnum stat, int value)
    {
        switch (stat)
        {
            case StatsEnum.MAX_HEALTH:
                maxHealth += value;
                break;
            case StatsEnum.HEALTH:
                health += value;
                break;
            case StatsEnum.DEFENSE:
                defense += value;
                break;
            case StatsEnum.MAGIC_DEFENSE:
                magicDefense += value;
                break;
            case StatsEnum.ATTACK:
                attack += value;
                break;
            case StatsEnum.MAGIC_ATTACK:
                magicAttack += value;
                break;
            case StatsEnum.SPEED:
                speed += value;
                break;
            case StatsEnum.SHIELD:
                shield += value;
                break;
            case StatsEnum.ACCURACY:
                accuracy += value;
                break;
            case StatsEnum.BLOCK_CHANCE:
                blockChance += value;
                break;
            case StatsEnum.DODGE_CHANCE:
                dodgeChance += value;
                break;
            case StatsEnum.CRIT_CHANCE:
                critChance += value;
                break;
            case StatsEnum.CRIT_DAMAGE_MULTIPLIER:
                critDamageMultiplier += value;
                break;

        }
    }
}
