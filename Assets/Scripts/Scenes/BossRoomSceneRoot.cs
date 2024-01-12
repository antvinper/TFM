using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BossRoomSceneRoot : SceneRoot
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

        Debug.Log("TODO?");

        ActivatePlayerController();
        InGameHUD.Instance.Setup();
    }
}
