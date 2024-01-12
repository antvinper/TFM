using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ImprovementRoomSceneRoot : SceneRoot
{
    private void Start()
    {
        Initialize();
    }

    public override async Task Initialize()
    {
        await new WaitUntil(() => IsInitialized);
        
        ActivatePlayerController();
        InGameHUD.Instance.Setup();

        Debug.Log("TODO");
    }
}
