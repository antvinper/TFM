using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    private static MainMenuManager instance;
    public static MainMenuManager Instance => instance;

    private void Awake()
    {
        instance = this;
    }

    public void NewGame()
    {
        Debug.Log("Initiating New Game...");
        GameManager.Instance.NewGame();
    }

    public List<GameMinModel> GetAllDataSaved()
    {
        return GameManager.Instance.GetAllFilesForLoad();
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
