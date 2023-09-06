using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterModelStats
{
    //public int MaxHealth { get; set; }
    public int Health { get; }
    public int Defense { get; }
    public int MagicDefense { get; }
    public int Attack { get; }
    public int MagicAttack { get; }
    public int Speed { get; }
    public int Shield { get; }
    public int Accuracy { get; }
    public int BlockChance { get;}
    public int DodgeChance { get; }
    public int CritChance { get; }
    public int CritDamageMultiplier { get; }

}
