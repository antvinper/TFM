using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MainMenuCanvasController : MonoBehaviour
{

    [SerializeField] private MainMenuCanvasView mainMenuCanvasView;
    public async Task LoadPanelLoad()
    {
        mainMenuCanvasView.LoadPanelLoad(await MainMenuManager.Instance.GetAllDataSaved());
    }

    public void LoadMainMenuPanel()
    {
        mainMenuCanvasView.LoadMainMenuPanel();
    }
}
