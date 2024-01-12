using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class StartMenu : Menu
{

    [Header("TextsToChange")]
    [SerializeField] private TextMeshProUGUI saveFileText;

    [Header("UiElements")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private GameObject optionsMenu;

    public void Setup()
    {
        mainMenuManager.SetCurrentLayout(this, newGameButton.gameObject);
    }

    public override void PerformAction(EventSystem eventSystem)
    {
        switch (eventSystem.currentSelectedGameObject.name)
        {
            case "New":
                //mainMenuManager.OpenNewGameMenu(this.gameObject);
                break;
            case "Continue":
                mainMenuManager.OpenFileSelectMenu(this.gameObject);
                break;
            case "Options":
                mainMenuManager.OpenOptionsMenu(this.gameObject);
                break;
        }
    }

    public void ContinueRun()
    {
        Debug.Log("Continuando la run del archivo de guardado");
    }
    
    public void NewRun(InputAction.CallbackContext context)
    {
        Debug.Log("Comenzando una nueva run en el archivo de guardado");
    }

    public void OpenOptions()
    {
        gameObject.SetActive(false);
        optionsMenu.SetActive(true);
    }
}
