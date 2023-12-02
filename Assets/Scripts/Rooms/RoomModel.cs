using CompanyStats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoomModel
{
    [SerializeField] RoomDefinition roomDefinition;
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private List<Wave> waves;

    private int actualWave = 1;
    public int ActualWave
    {
        get => actualWave;
    }

    /*public List<Transform> SpawnPoints
    {
        get => spawnPoints;
    }*/

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

        switch (roomDefinition.RewardType)
        {
            case RewardsEnum.RUPEES:
                roomReward = new RoomReward(roomDefinition.RewardType, GetRupeesAmount(luckyValue));
                break;
            case RewardsEnum.HEAL:
                roomReward = new RoomReward(roomDefinition.RewardType, GetHealAmount(luckyValue));
                roomReward.SetHealSkill(roomDefinition.HealSkill);
                break;
            case RewardsEnum.SOULFRAGMENTS:
                roomReward = new RoomReward(roomDefinition.RewardType, GetSoulFragmentsAmount(luckyValue));
                break;
        }

        return roomReward;
    }

    public int GetSoulFragmentsAmount(int luckyValue)
    {
        int preValue = Random.Range(roomDefinition.SoulFragmentsMinAmount, roomDefinition.SoulFragmentsMaxAmount + 1);

        return GetFinalValue(preValue, luckyValue);
    }

    public int GetHealAmount(int luckyValue)
    {
        int preValue = Random.Range(roomDefinition.HealMinAmount, roomDefinition.HealMaxAmount + 1);

        return GetFinalValue(preValue, luckyValue);
    }

    public int GetRupeesAmount(int luckyValue)
    {
        int preValue = Random.Range(roomDefinition.RupeesMinAmount, roomDefinition.RupeesMaxAmount + 1);

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
