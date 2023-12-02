using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class RoomManager : SingletonMonoBehaviour<RoomManager>
{
    [SerializeField] private RoomController roomController;
    private PlayerController playerController;


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
