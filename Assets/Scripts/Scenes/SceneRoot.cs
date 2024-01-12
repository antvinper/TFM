using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneRoot : MonoBehaviour
{
    [SerializeField] private bool saveOnInit;
    public bool IsInitialized { get; private set; }

    private void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    protected void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
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
}
