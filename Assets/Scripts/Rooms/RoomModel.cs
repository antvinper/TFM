using CompanyStats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoomModel
{
    private Room roomDefinition;
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private Wave wave;
    private RewardsEnum rewardType;

    private int totalWaves = 0;
    private int actualWave = 1;
    private int[] nEnemiesToSpawnPerWave;

    public int ActualWave
    {
        get => actualWave;
    }

    public void Setup(RewardsEnum rewardType, Room roomDefinition)
    {
        this.rewardType = rewardType;
        this.roomDefinition = roomDefinition;

        float numero = GameManager.Instance.RunLevel;
        float basePersonalizada = 1.75f;

        float logaritmoBasePersonalizada = Mathf.Log(numero+1) / Mathf.Log(basePersonalizada);
        totalWaves = Mathf.FloorToInt(logaritmoBasePersonalizada);

        nEnemiesToSpawnPerWave = new int[totalWaves];

        FillEnemiesToSpawnPerWave();
    }

    private void FillEnemiesToSpawnPerWave()
    {
        int totalEnemiesToSpawn = GetNEnemiesToSpawn();

        while (totalEnemiesToSpawn > 0)
        {
            for (int i = totalWaves - 1; i >= 0; --i)
            {
                nEnemiesToSpawnPerWave[i] += 1;
                --totalEnemiesToSpawn;
                if (totalEnemiesToSpawn == 0)
                {
                    break;
                }
            }
        }
    }

    public List<Transform> SpawnPoints
    {
        get => spawnPoints;
    }

    public bool IsTheLastWave()
    {
        bool isTheLastWave = false;
        if(actualWave == totalWaves)
        {
            isTheLastWave = true;
        }
        else
        {
            ++actualWave;
        }

        return isTheLastWave;
    }

    private int GetNEnemiesToSpawn()
    {
        return Mathf.FloorToInt(GameManager.Instance.RunLevel * 1.5f);
        //return waves[actualWave - 1].NumberOfEnemiesToSpawn;
    }

    public int GetNEnemiesToSpawnInWave()
    {
        return nEnemiesToSpawnPerWave[actualWave - 1] <= spawnPoints.Count ? nEnemiesToSpawnPerWave[actualWave - 1]: spawnPoints.Count;
    }

    public GameObject GetWaveRandomEnemy()
    {
        int randomIndex = Random.Range(0, wave.Enemies.Count);

        return wave.Enemies[randomIndex];
    }

    public List<GameObject> GetEnemiesWave()
    {
        return wave.Enemies;
    }

    public RoomReward GetReward(PlayerController playerController, int runLevel)
    {
        RoomReward roomReward = null;

        int luckyValue = playerController.GetStatValue(StatNames.LUCKY, StatParts.ACTUAL_VALUE);

        switch (rewardType)
        {
            case RewardsEnum.RUPEES:
                roomReward = new RoomReward(rewardType, GetRupeesAmount(roomDefinition.RupeesMinAmount, runLevel, luckyValue));
                break;
            case RewardsEnum.HEAL:
                roomReward = new RoomReward(rewardType, GetHealAmount(roomDefinition.HealMinAmount, runLevel, luckyValue));
                roomReward.SetHealSkill(roomDefinition.HealSkill);
                break;
            case RewardsEnum.SOULFRAGMENTS:
                roomReward = new RoomReward(rewardType, GetSoulFragmentsAmount(roomDefinition.SoulFragmentsMinAmount, runLevel, luckyValue));
                break;
        }

        return roomReward;
    }

    public int GetSoulFragmentsAmount(int soulFragmentsMinAmount, int runLevel, int luckyValue)
    {
        int preValue = Random.Range(soulFragmentsMinAmount, soulFragmentsMinAmount + ((runLevel * 3) + 1));

        return GetFinalValue(preValue, luckyValue);
    }

    public int GetHealAmount(int healMinAmount, int runLevel, int luckyValue)
    {
        int preValue = Random.Range(roomDefinition.HealMinAmount, healMinAmount + ( (runLevel * 10) + 1));

        return GetFinalValue(preValue, luckyValue);
    }

    public int GetRupeesAmount(int rupeesMinAmount, int runLevel, int luckyValue)
    {
        int preValue = Random.Range(roomDefinition.RupeesMinAmount, rupeesMinAmount + ( (runLevel * 6) + 1));

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
        wave.AddEnemyKilled();
    }

    public void RestartEnemiesKilled()
    {
        wave.RestartEnemiesKilled();
    }

}
