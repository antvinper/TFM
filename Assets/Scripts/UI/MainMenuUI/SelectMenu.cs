using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectMenu : Menu
{
    [Header("UiElements")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button backButton;
    private FileFolder fileFolder;

    public void Setup(FileFolder fileFolder)
    {
        this.fileFolder = fileFolder;
        mainMenuManager.SetCurrentLayout(this, newGameButton.gameObject);
    }

    public override void PerformAction(EventSystem eventSystem)
    {
        switch (eventSystem.currentSelectedGameObject.name)
        {
            case "StartButton":
                fileFolder.StartGame();
                break;
            case "BackButton":
                mainMenuManager.OpenFileSelectMenu(gameObject);
                break;
        }
    }
}
