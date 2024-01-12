using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MainMenuController : MonoBehaviour
{
    //[SerializeField] private FileSelectMenu fileSelect;

    public InputActionReference submitReference;
    [SerializeField] protected EventSystem eventSystem;

    //private static MainMenuController instance;
    //public static MainMenuController Instance => instance;

    [SerializeField] PressAnythingMenu pressAnythingMenu;
    //[SerializeField] StartMenu startMenu;
    [SerializeField] SelectMenu selectMenu;
    [SerializeField] OptionsMenu optionsMenu;
    [SerializeField] FileSelectMenu fileSelectMenu;
    [SerializeField] GameObject playerInMenu;

    private Menu currentMenu;

    /*private void Awake()
    {
        instance = this;
    }*/

    public void OpenNewGameMenu(GameObject menuToClose, FileFolder fileFolderSelected)
    {
        menuToClose.SetActive(false);
        playerInMenu.SetActive(true);
        selectMenu.gameObject.SetActive(true);
        selectMenu.Setup(fileFolderSelected);
    }
    /*public void OpenStartMenu(GameObject menuToClose)
    {
        menuToClose.SetActive(false);
        playerInMenu.SetActive(false);
        startMenu.gameObject.SetActive(true);
        startMenu.Setup();
    }*/
    public void OpenFileSelectMenu(GameObject menuToClose)
    {
        menuToClose.SetActive(false);
        fileSelectMenu.gameObject.SetActive(true);
        fileSelectMenu.Setup();
    }
    public void OpenOptionsMenu(GameObject menuToClose)
    {
        menuToClose.SetActive(false);
        optionsMenu.gameObject.SetActive(true);
        optionsMenu.Setup();
    }

    private void PerformStartMenuActions(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase.Equals(InputActionPhase.Performed))
        {
            currentMenu.PerformAction(eventSystem);
        }
    }
    
    public async Task SetCurrentLayout(Menu menuLayout, GameObject buttonActive)
    {
        await new WaitForSeconds(0.1f);

        if(menuLayout is StartMenu)
        {
            currentMenu = menuLayout;
        }
        else if(menuLayout is SelectMenu)
        {
            currentMenu = selectMenu;
        }
        else if(menuLayout is FileSelectMenu)
        {
            currentMenu = fileSelectMenu;
        }
        else if(menuLayout is OptionsMenu)
        {
            currentMenu = optionsMenu;
        }
        else
        {
            currentMenu = pressAnythingMenu;
        }

        submitReference.action.performed += this.PerformStartMenuActions;
        eventSystem.SetSelectedGameObject(buttonActive);
    }

    private void OnDestroy()
    {
        submitReference.action.performed -= this.PerformStartMenuActions;
    }

    public async Task<List<GameModel>> GetAllDataSaved()
    {
        return await GameManager.Instance.GetAllFilesForLoad();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
