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

    [JsonProperty]
    public float MaxHealth { 
        get => maxHealth + statsIncrement.MaxHealth; 
        set => maxHealth = value; 
    }
    [JsonProperty]
    public float Health { 
        get => health + statsIncrement.Health; 
        set => health = value; 
    }
    [JsonProperty]
    public int Defense { 
        get => defense + statsIncrement.Defense; 
        set => defense = value; 
    }
    [JsonProperty]
    public int MagicDefense { 
        get => magicDefense + statsIncrement.MagicDefense; 
        set => magicDefense = value; 
    }
    [JsonProperty]
    public int Attack { 
        get => attack + statsIncrement.Attack; 
        set => attack = value; 
    }
    [JsonProperty]
    public int MagicAttack { 
        get => magicAttack + statsIncrement.MagicAttack;
        set => magicAttack = value;
    }
    [JsonProperty]
    public int Speed { 
        get => speed + statsIncrement.Speed;
        set => speed = value;
    }
    [JsonProperty]
    public int Shield { 
        get => shield + statsIncrement.Shield; 
        set => shield = value; 
    }
    [JsonProperty]
    public int Accuracy { 
        get => accuracy + statsIncrement.Accuracy; 
        set => accuracy = value; 
    }
    [JsonProperty]
    public int BlockChance { 
        get => blockChance + statsIncrement.BlockChance; 
        set => blockChance = value; 
    }
    public int DodgeChance { 
        get => dodgeChance + statsIncrement.DodgeChance;
        set => dodgeChance = value;
    }
    [JsonProperty]
    public int CritChance { 
        get => critChance + statsIncrement.CritChance; 
        set => critChance = value; 
    }
    [JsonProperty]
    public int CritDamageMultiplier { 
        get => critDamageMultiplier + statsIncrement.CritDamageMultiplier; 
        set => critDamageMultiplier = value; 
    }

    [JsonProperty]
    public StatsIncrement StatsIncrement
    {
        get => statsIncrement;
        set => statsIncrement = value;
    }

    public void TakeDamage(float value)
    {
        float realDamage = value - Defense;
        float finalDamage = realDamage < 0 ? 0 : realDamage;

        Health -= finalDamage;
    }
}
