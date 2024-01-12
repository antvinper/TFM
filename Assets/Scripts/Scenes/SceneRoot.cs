using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneRoot : MonoBehaviour
{
    [SerializeField] private bool saveOnInit;
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
            Debug.Log("GameSaved");
        }
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

    protected void ActivatePlayerController()
    {
        playerController = GameManager.Instance.GetPlayerController();
        playerController.ActivateControls();
    }


}
