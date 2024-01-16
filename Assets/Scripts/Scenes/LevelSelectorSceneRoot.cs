using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LevelSelectorSceneRoot : SceneRoot
{

    private void Start()
    {
        Initialize();
    }

    public override async Task Initialize()
    {
        await new WaitUntil(() => IsInitialized);
        GameManager.Instance.GetPlayerController().DeActivateControls(true);
        await fadeController.FadeOut();
    }
}
