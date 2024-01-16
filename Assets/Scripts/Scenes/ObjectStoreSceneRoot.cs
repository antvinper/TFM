using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ObjectStoreSceneRoot : SceneRoot
{
    [SerializeField] ShopController shopController;

    private void Start()
    {
        Initialize();
    }

    public override async Task Initialize()
    {
        await new WaitUntil(() => IsInitialized);

        await ActivatePlayerController();
        InGameHUD.Instance.Setup();

        shopController.CreateShop(playerController);
    }
}
