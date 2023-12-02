using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    [SerializeField] RoomModel model;

    List<int> spawnPointsAvailable;

    List<GameObject> instantiatedEnemies;

    [SerializeField] BoxCollider nextLevelCollider;
    [SerializeField] MeshRenderer nextLevelDoorMesh;


    public async Task StartNewWave()
    {
        Debug.Log("#WAVE Starting wave: " + model.ActualWave);
        model.RestartEnemiesKilled();

        instantiatedEnemies = new List<GameObject>();

        spawnPointsAvailable = new List<int>();
        for(int i = 0; i < model.SpawnPoints.Count; ++i)
        {
            spawnPointsAvailable.Add(i);
        }

        for(int i = 0; i < model.GetNEnemiesToSpawn(); ++i)
        {
            GameObject go = Instantiate(model.GetWaveRandomEnemy(), GetRandomSpawnPoint());
            instantiatedEnemies.Add(go);
        }

    }

    private Transform GetRandomSpawnPoint()
    {
        int randomSpawnPointsIndex = spawnPointsAvailable[Random.Range(0, spawnPointsAvailable.Count)];
        Transform spawnPoint = model.SpawnPoints[randomSpawnPointsIndex];
        spawnPointsAvailable.Remove(randomSpawnPointsIndex);

        return spawnPoint;
    }

    private RoomReward GetReward(PlayerController playerController)
    {
        return model.GetReward(playerController);
    }

    public async Task GetRoomRewards(PlayerController playerController)
    {
        RoomReward roomReward = GetReward(playerController);

        switch (roomReward.RewardType)
        {
            case RewardsEnum.RUPEES:
                playerController.AddRupees(roomReward.Value);
                break;
            case RewardsEnum.SOULFRAGMENTS:
                playerController.AddSoulFragments(roomReward.Value);
                break;
            case RewardsEnum.HEAL:
                roomReward.ApplySkill(playerController);
                break;
        }

        Debug.Log("#REWARDS " + roomReward.RewardType + " -> " + roomReward.Value);
    }

    public async Task OnEnemyKilled(EnemyController enemy, PlayerController playerController)
    {
        GameObject goToErase = null;

        foreach(GameObject enemyGo in instantiatedEnemies)
        {
            if (enemy.Equals(enemyGo.transform.GetComponent<EnemyController>()))
            {
                goToErase = enemyGo;
                break;
            }
        }

        if(goToErase != null)
        {
            instantiatedEnemies.Remove(goToErase);
        }
        if (instantiatedEnemies.Count.Equals(0))
        {
            //TODO -> Comprobar si es la última wave.
            //Si lo es, dar rewards, sino, instanciar la siguiente wave
            if (model.IsTheLastWave())
            {
                GetRoomRewards(playerController);
                
                nextLevelDoorMesh.enabled = false;
                nextLevelCollider.enabled = true;
            }
            else
            {
                await new WaitForSeconds(0.5f);
                StartNewWave();
            }
        }
    }
}
