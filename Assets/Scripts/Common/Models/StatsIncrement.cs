using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class StatsIncrement : ICharacterModelStats
{
    [SerializeField] private float maxHealth = 0;
    [SerializeField] private float health = 0;
    [SerializeField] private int defense = 0;
    [SerializeField] private int magicDefense = 0;
    [SerializeField] private int attack = 0;
    [SerializeField] private int magicAttack = 0;
    [SerializeField] private int speed = 0;
    [SerializeField] private int shield = 0;
    [SerializeField] [Range(0, 100)] private int accuracy = 0;
    [SerializeField] [Range(0, 100)] private int blockChance = 0;
    [SerializeField] [Range(0, 100)] private int dodgeChance = 0;
    [SerializeField] [Range(0, 100)] private int critChance = 0;
    [SerializeField] [Range(0, 100)] private int critDamageMultiplier = 0;

    [JsonProperty]
    public float MaxHealth
    {
        get => maxHealth;
        set => maxHealth = value;
    }
    [JsonProperty]
    public float Health
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
}
