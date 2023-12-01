using CompanyStats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoomModel
{
    [SerializeField] private RoomTypesEnum roomType;
    [SerializeField] private List<GameObject> enemiesPrefabs;
    [SerializeField] private RewardsEnum rewardType;
    [SerializeField] private int rupeesMinAmount;
    [SerializeField] private int rupeesMaxAmount;
    [SerializeField] private int soulFragmentsMinAmount;
    [SerializeField] private int soulFragmentsMaxAmount;
    [SerializeField] private int healMinAmount;
    [SerializeField] private int healMaxAmount;
    [SerializeField] private SkillDefinition healSkill;

    private int enemiesDied = 0;


    public RoomReward GetReward(PlayerController playerController)
    {
        RoomReward roomReward = null;

        int luckyValue = playerController.GetStatValue(StatNames.LUCKY, StatParts.ACTUAL_VALUE);

        switch (rewardType)
        {
            case RewardsEnum.RUPEES:
                roomReward = new RoomReward(rewardType, GetRupeesAmount(luckyValue));
                break;
            case RewardsEnum.HEAL:
                roomReward = new RoomReward(rewardType, GetHealAmount(luckyValue));
                roomReward.SetHealSkill(healSkill);
                break;
            case RewardsEnum.SOULFRAGMENTS:
                roomReward = new RoomReward(rewardType, GetSoulFragmentsAmount(luckyValue));
                break;
        }

        return roomReward;
    }

    public int GetSoulFragmentsAmount(int luckyValue)
    {
        int preValue = Random.Range(soulFragmentsMinAmount, soulFragmentsMaxAmount + 1);

        return GetFinalValue(preValue, luckyValue);
    }

    public int GetHealAmount(int luckyValue)
    {
        int preValue = Random.Range(healMinAmount, healMaxAmount + 1);

        return GetFinalValue(preValue, luckyValue);
    }

    public int GetRupeesAmount(int luckyValue)
    {
        int preValue = Random.Range(rupeesMinAmount, rupeesMaxAmount + 1);

        return GetFinalValue(preValue, luckyValue);
    }

    private int GetFinalValue(int preValue, int luckyValue)
    {
        int preLuckyValue = (int)System.Math.Round((preValue * luckyValue * 0.01f));
        int value = (preValue + preLuckyValue);

        return value;
    }

    public bool OnEnemyKilled(EnemyController enemy)
    {
        bool areAllEnemiesKilled = false;
        GameObject goToErase = null;
        foreach(GameObject go in enemiesPrefabs)
        {
            if (enemy.Equals(go.transform.GetComponent<EnemyController>()))
            {
                ++enemiesDied;
                
                goToErase = go;
            }
        }
        if(goToErase != null)
        {
            enemiesPrefabs.Remove(goToErase);
        }
        if (enemiesPrefabs.Count.Equals(0))
        {
            areAllEnemiesKilled = true;
        }
        return areAllEnemiesKilled;
    }

}
