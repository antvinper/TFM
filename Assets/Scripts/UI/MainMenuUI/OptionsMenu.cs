using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OptionsMenu : Menu
{
    [Header("UiElements")]
    [SerializeField] private Button backButton;
    [SerializeField] private TMP_Dropdown dropDown;

    public void Setup()
    {
        mainMenuManager.SetCurrentLayout(this, backButton.gameObject);
    }

    public override void PerformAction(EventSystem eventSystem)
    {
        if (eventSystem.currentSelectedGameObject.name.Equals("BackButton"))
        {
            mainMenuManager.OpenFileSelectMenu(this.gameObject);
        }
    }
}
