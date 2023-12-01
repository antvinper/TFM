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
        StartWave();
    }

    public async Task StartWave()
    {
        roomController.StartNewWave();
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
