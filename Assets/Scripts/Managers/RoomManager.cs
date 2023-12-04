using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class RoomManager : SingletonMonoBehaviour<RoomManager>
{
    private RoomController roomController;
    private PlayerController playerController;


    private void Start()
    {
        this.playerController = GameManager.Instance.GetPlayerController();
        this.roomController = GameObject.FindGameObjectWithTag("RoomController").GetComponent<RoomController>();
        StartRoomWaves();
    }

    public async Task StartRoomWaves()
    {
        roomController.StartRoomWaves();
    }

    
    /*public void Setup(PlayerController playerController)
    {
        this.playerController = playerController;
    }*/

    public void OnEnemyKilled(EnemyController enemy)
    {
        roomController.OnEnemyKilled(enemy, playerController);
    }
}
