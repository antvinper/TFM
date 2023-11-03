using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private FileSelect fileSelect;

    public InputActionReference submitReference;
    [SerializeField] protected EventSystem eventSystem;

    private static MainMenuManager instance;
    public static MainMenuManager Instance => instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Debug.Log("Starting MainMenuMager...");
        submitReference.action.performed += this.StartGameAction;
    }

    private void StartGameAction(InputAction.CallbackContext context)
    {
        eventSystem.currentSelectedGameObject.GetComponentInParent<FileFolder>().StartGame();
    }

    private void OnDestroy()
    {
        submitReference.action.performed -= this.StartGameAction;
    }

    public async Task<List<GameModel>> GetAllDataSaved()
    {
        return await GameManager.Instance.GetAllFilesForLoad();
    }

    public void LoadGame()
    {
        Debug.Log("Loading Game...");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
