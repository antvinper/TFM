using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;

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

    [SerializeField] private StatsIncrement statsIncrement;
    [SerializeField] private StatsIncrement statsIncrementInRun;

    [JsonProperty]
    public float MaxHealth { 
        get => maxHealth + statsIncrement.MaxHealth + statsIncrementInRun.MaxHealth; 
        set => maxHealth = value; 
    }
    [JsonIgnore]
    public float Health { 
        get => health; 
        set 
        {
            Debug.Log("AA");
            health = value; 
        }
    }
    [JsonProperty]
    public int Defense { 
        get => defense + statsIncrement.Defense + statsIncrementInRun.Defense; 
        set => defense = value; 
    }
    [JsonProperty]
    public int MagicDefense { 
        get => magicDefense + statsIncrement.MagicDefense + statsIncrementInRun.MagicDefense; 
        set => magicDefense = value; 
    }
    [JsonProperty]
    public int Attack { 
        get => attack + statsIncrement.Attack + statsIncrementInRun.Attack; 
        set => attack = value; 
    }
    [JsonProperty]
    public int MagicAttack { 
        get => magicAttack + statsIncrement.MagicAttack + statsIncrementInRun.MagicAttack;
        set => magicAttack = value;
    }
    [JsonProperty]
    public int Speed { 
        get => speed + statsIncrement.Speed + statsIncrementInRun.Speed;
        set => speed = value;
    }
    [JsonProperty]
    public int Shield { 
        get => shield + statsIncrement.Shield + statsIncrementInRun.Shield; 
        set => shield = value; 
    }
    [JsonProperty]
    public int Accuracy { 
        get => accuracy + statsIncrement.Accuracy + statsIncrementInRun.Accuracy; 
        set => accuracy = value; 
    }
    [JsonProperty]
    public int BlockChance { 
        get => blockChance + statsIncrement.BlockChance + statsIncrementInRun.BlockChance; 
        set => blockChance = value; 
    }
    public int DodgeChance { 
        get => dodgeChance + statsIncrement.DodgeChance + statsIncrementInRun.DodgeChance;
        set => dodgeChance = value;
    }
    [JsonProperty]
    public int CritChance { 
        get => critChance + statsIncrement.CritChance + statsIncrementInRun.CritChance; 
        set => critChance = value; 
    }
    [JsonProperty]
    public int CritDamageMultiplier { 
        get => critDamageMultiplier + statsIncrement.CritDamageMultiplier + statsIncrementInRun.CritDamageMultiplier; 
        set => critDamageMultiplier = value; 
    }

    [JsonProperty]
    public StatsIncrement StatsIncrement
    {
        get => statsIncrement;
        set => statsIncrement = value;
    }

    [JsonIgnore]
    public StatsIncrement StatsIncrementInRun
    {
        get => statsIncrementInRun;
        set => statsIncrementInRun = value;
    }

    /**
     * TODO ?
     * Si el TakeDamage fuese diferente en el personaje y en los enemigos
     * debe implementarse en cada uno por separado
     */
    public bool TakeDamage(float value)
    {
        float realDamage = value - Defense;
        float finalDamage = realDamage < 0 ? 0 : realDamage;

        health -= finalDamage;

        return CheckIsDead();
    }
    public bool TakePercentualDamage(float value)
    {
        health += value;

        return CheckIsDead();
    }

    public void Heal(float value)
    {
        float finalHeal = Math.Clamp(value + Health, 0, maxHealth - Health);
        health += finalHeal;
        Debug.Log(finalHeal);
    }

    private bool CheckIsDead()
    {
        return health <= 0;
    }

}
