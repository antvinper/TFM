using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneRoot : MonoBehaviour
{
    [SerializeField] private bool saveOnInit;
    [SerializeField] private Transform playerSpawnPoint;
    [SerializeField] protected FadeController fadeController;

    protected PlayerController playerController;
    public bool IsInitialized { get; private set; }

    private void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    protected void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        Debug.Log("#SCENE SceneLoaded: " + name);
        if (saveOnInit)
        {
            GameManager.Instance.SaveGame();
            //Debug.Log("GameSaved");
        }
        SceneManager.Instance.Setup(this);
        IsInitialized = true;
    }

    public virtual async Task Initialize()
    {
        await new WaitUntil(() => IsInitialized == true);

    }

    private void OnDestroy()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    protected async Task ActivatePlayerController()
    {
        playerController = GameManager.Instance.GetPlayerController();
        playerController.SetStartPosition(playerSpawnPoint);
        Debug.Log("PlayerPosition setted");
        await playerController.ActivateControls();
        //await new WaitForSeconds(1.0f);
        await fadeController.FadeOut();
    }

    public async Task FadeIn()
    {
        await fadeController.FadeIn();
    }


}
