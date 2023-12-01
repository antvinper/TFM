using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class RoomManager : SingletonMonoBehaviour<RoomManager>
{
    private RoomController roomController;
    private PlayerController playerController;

    public RoomController RoomController
    {
        set => this.roomController = value;
    }

    private void Start()
    {
        this.playerController = GameManager.Instance.GetPlayerController();
    }

    
    /*public void Setup(PlayerController playerController)
    {
        this.playerController = playerController;
    }*/

    public async Task GetRoomRewards()
    {
        RoomReward roomReward = roomController.GetReward(playerController);

        switch (roomReward.RewardType)
        {
            case RewardsEnum.RUPEES:
                playerController.AddRupees(roomReward.Value);
                break;
            case RewardsEnum.SOULFRAGMENTS:
                playerController.AddSoulFragments(roomReward.Value);
                Debug.Log("TODO");
                break;
            case RewardsEnum.HEAL:
                roomReward.ApplySkill(playerController);
                break;
        }

        Debug.Log("#REWARDS " + roomReward.RewardType + " -> " + roomReward.Value);
    }

    public void OnEnemyKilled(EnemyController enemy)
    {
        bool areAllEnemiesKilled = this.roomController.OnEnemyKilled(enemy);
        if (areAllEnemiesKilled)
        {
            GetRoomRewards();
        }
    }
}
