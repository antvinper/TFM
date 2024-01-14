using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FileSelectMenu : Menu
{
    private List<FileFolder> fileFolders;
    private bool hasBeenFilled = false;
    [SerializeField] private MainMenuController mainMenuController;

    [SerializeField] private Button firstSelectedButton;
    public void Setup()
    {
        if (!hasBeenFilled)
        {
            fileFolders = new List<FileFolder>(GetComponentsInChildren<FileFolder>());
            FillFileFolders();
        }

        mainMenuManager.SetCurrentLayout(this, firstSelectedButton.gameObject);
    }

    public override void PerformAction(EventSystem eventSystem)
    {
        //Debug.Log(eventSystem.currentSelectedGameObject.name);
        if (eventSystem.currentSelectedGameObject.name.Equals("OptionsButton"))
        {
            mainMenuManager.OpenOptionsMenu(this.gameObject);
        }
        else
        {
            FileFolder fileFolderSelected = eventSystem.currentSelectedGameObject.GetComponentInParent<FileFolder>();
            if (fileFolderSelected.IsNewGame)
            {
                mainMenuManager.OpenNewGameMenu(this.gameObject, fileFolderSelected);
            }
            else
            {
                fileFolderSelected.StartGame();
            }
            
        }
    }

    public async Task FillFileFolders()
    {
        //await new WaitUntil(() => MainMenuController.Instance != null);
        
        //Debug.Log("Filling file folders");
        List<GameModel>gameModels = await mainMenuController.GetAllDataSaved();

        int slotIndex = 0;
        foreach(GameModel gameModel in gameModels)
        {
            fileFolders[gameModel.SlotIndex].FillFileFolderForLoadGame(gameModel);
        }
        for (int i = gameModels.Count; i <= 3; ++i)
        {
            fileFolders[i].FillFileFolderForNewGame(i);
        }

        hasBeenFilled = true;
    }

}
