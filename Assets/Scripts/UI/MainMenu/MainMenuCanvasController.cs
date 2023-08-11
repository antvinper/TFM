using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCanvasController : MonoBehaviour
{

    [SerializeField] private MainMenuCanvasView mainMenuCanvasView;
    public void LoadPanelLoad()
    {
        mainMenuCanvasView.LoadPanelLoad(MainMenuManager.Instance.GetAllDataSaved());
    }

    public void LoadMainMenuPanel()
    {
        mainMenuCanvasView.LoadMainMenuPanel();
    }
}
