using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCanvasView : MonoBehaviour
{
    [SerializeField] private GameObject LoadPanel;
    [SerializeField] private GameObject MainMenuPanel;

    public void LoadPanelLoad(List<GameMinModel> savedData)
    {
        UnloadMainMenuPanel();
        LoadPanel.SetActive(true);
        LoadPanel.GetComponent<PanelController>().Setup<GameMinModel>(savedData);
    }

    public void LoadMainMenuPanel()
    {
        UnloadPanelLoad();
        MainMenuPanel.SetActive(true);
    }

    private void UnloadPanelLoad()
    {
        LoadPanel.SetActive(false);
    }

    private void UnloadMainMenuPanel()
    {
        MainMenuPanel.SetActive(false);
    }

}
