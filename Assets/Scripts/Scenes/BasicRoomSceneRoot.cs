using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BasicRoomSceneRoot : SceneRoot
{
    [SerializeField] private RoomController roomController;
    private PlayerController playerController;

    private void Start()
    {
        Initialize();
    }

    public override async Task Initialize()
    {
        await new WaitUntil(() => IsInitialized);
        playerController = GameManager.Instance.GetPlayerController();
        roomController.Setup(playerController);
        roomController.StartRoomWaves();  
    }
}
