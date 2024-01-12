using CompanyStats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Room : ScriptableObject
{
    [SerializeField] private RoomTypesEnum roomType;
    internal bool hasReward;
    [SerializeField] private int rupeesMinAmount;
    [SerializeField] private int soulFragmentsMinAmount;
    [SerializeField] private int healMinAmount;
    [SerializeField] private SkillDefinition healSkill;

    public int RupeesMinAmount
    {
        get => rupeesMinAmount;
    }

    public int SoulFragmentsMinAmount
    {
        get => soulFragmentsMinAmount;
    }

    public int HealMinAmount
    {
        get => healMinAmount;
    }

    public SkillDefinition HealSkill
    {
        get => healSkill;
    }
}
