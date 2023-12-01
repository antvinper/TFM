using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    [SerializeField] RoomModel model;

    private void Start()
    {
        RoomManager.Instance.RoomController = this; 
    }

    public RoomReward GetReward(PlayerController playerController)
    {
        return model.GetReward(playerController);
    }

    public bool OnEnemyKilled(EnemyController enemy)
    {
        return model.OnEnemyKilled(enemy);
    }
}
