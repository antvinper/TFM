using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModel : MonoBehaviour
{
    [SerializeField] private int life;
    [SerializeField] private int strength;
    [SerializeField] private int defense;

    public int Life
    {
        get { return life; }
        set { life = value; }
    }

    public int Strength
    {
        get { return strength; }
        set { strength = value; }
    }

    public int Defense
    {
        get { return defense; }
        set { defense = value; }
    }
}
