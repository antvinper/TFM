using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MainMenuSceneRoot : SceneRoot
{
    [SerializeField] private StartMenu startMenu;

    private void Start()
    {
        Initialize();
    }

    public override async Task Initialize()
    {
        await new WaitUntil(() => IsInitialized);
        this.startMenu.Setup();
    }
}
