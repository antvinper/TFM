using CompanyStats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomReward
{
    private RewardsEnum rewardType;
    private int value;
    private SkillDefinition healSkill;

    public RewardsEnum RewardType
    {
        get => rewardType;
    }
    public int Value
    {
        get => value;
    }

    public RoomReward(RewardsEnum rewardType, int value)
    {
        this.rewardType = rewardType;
        this.value = value;
    }

    public void SetHealSkill(SkillDefinition skill)
    {
        healSkill = skill;
        healSkill.temporalInstantEffects[0].Value = value;
    }

    public void ApplySkill(CompanyCharacterController target)
    {
        healSkill.ProcessSkill(target, target);
    }
}
