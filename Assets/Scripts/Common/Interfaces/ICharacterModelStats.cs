using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterModelStats
{
    public float MaxHealth { get; set; }
    public float Health { get; set; }
    public int Defense { get; set; }
    public int MagicDefense { get; set; }
    public int Attack { get; set; }
    public int MagicAttack { get; set; }
    public int Speed { get; set; }
    public int Shield { get; set; }
    public int Accuracy { get; set; }
    public int BlockChance { get; set; }
    public int DodgeChance { get; set; }
    public int CritChance { get; set; }
    public int CritDamageMultiplier { get; set; }

}
