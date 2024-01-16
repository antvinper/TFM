using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class HighRoomSceneRoot : SceneRoot
{
    [SerializeField] private RoomController roomController;
    [SerializeField] private Room roomDefinition;
    private void Start()
    {
        Initialize();
    }

    public override async Task Initialize()
    {
        await new WaitUntil(() => IsInitialized);

        await ActivatePlayerController();
        InGameHUD.Instance.Setup();

        roomController.Setup(playerController);
        roomController.StartRoomWaves(GameManager.Instance.NextRoomReward, roomDefinition);
    }
}
