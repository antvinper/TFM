using CompanyStats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomDefinition", menuName = "TFM/Room/NormalRoomDefinition")]
public class RoomDefinition : ScriptableObject
{
    [SerializeField] private RoomTypesEnum roomType;
    [SerializeField] private RewardsEnum rewardType;
    [SerializeField] private int rupeesMinAmount;
    [SerializeField] private int rupeesMaxAmount;
    [SerializeField] private int soulFragmentsMinAmount;
    [SerializeField] private int soulFragmentsMaxAmount;
    [SerializeField] private int healMinAmount;
    [SerializeField] private int healMaxAmount;
    [SerializeField] private SkillDefinition healSkill;

    public int RupeesMinAmount
    {
        get => rupeesMinAmount;
    }
    public int RupeesMaxAmount
    {
        get => rupeesMaxAmount;
    }

    public int SoulFragmentsMinAmount
    {
        get => soulFragmentsMinAmount;
    }
    public int SoulFragmentsMaxAmount
    {
        get => soulFragmentsMaxAmount;
    }

    public int HealMinAmount
    {
        get => healMinAmount;
    }
    public int HealMaxAmount
    {
        get => healMaxAmount;
    }

    public RewardsEnum RewardType
    {
        get => rewardType;
    }

    public SkillDefinition HealSkill
    {
        get => healSkill;
    }
}
