using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PressAnythingMenu : Menu
{
    private void Start()
    {
        SetupAsync();
    }

    private async Task SetupAsync()
    {
        await new WaitForSeconds(0.05f);
        mainMenuManager.SetCurrentLayout(this, null);
    }

    //LO DEJO COMENTADO POR SI ACASO.

    //[SerializeField] GameObject[] uisToHide;
    //[SerializeField] GameObject uiToShow;
    /*void Update()
    {
        if (Input.anyKeyDown)
        {
            Debug.Log("Key pressed");
            foreach(GameObject ui in uisToHide)
            {
                ui.SetActive(false);
            }
            uiToShow.SetActive(true);
        }
    }*/

    public override void PerformAction(EventSystem eventSystem)
    {
        mainMenuManager.OpenFileSelectMenu(this.gameObject);
    }
}
