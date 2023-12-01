using CompanyStats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoomModel
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
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private List<Wave> waves;

    private int actualWave = 1;
    public int ActualWave
    {
        get => actualWave;
    }

    public List<Transform> SpawnPoints
    {
        get => spawnPoints;
    }

    public bool IsTheLastWave()
    {
        bool isTheLastWave = false;
        if(actualWave == waves.Count)
        {
            isTheLastWave = true;
        }
        else
        {
            ++actualWave;
        }

        return isTheLastWave;
    }

    public int GetNEnemiesToSpawn()
    {
        return waves[actualWave - 1].NumberOfEnemiesToSpawn;
    }

    public GameObject GetWaveRandomEnemy()
    {
        int randomIndex = Random.Range(0, waves[actualWave - 1].Enemies.Count);

        return waves[actualWave - 1].Enemies[randomIndex];
    }

    public List<GameObject> GetEnemiesWave()
    {
        return waves[actualWave - 1].Enemies;
    }

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

    public void AddEnemyKilled()
    {
        waves[actualWave - 1].AddEnemyKilled();
    }

    public void RestartEnemiesKilled()
    {
        waves[actualWave - 1].RestartEnemiesKilled();
    }

}
