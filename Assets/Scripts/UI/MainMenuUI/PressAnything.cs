using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressAnything : MonoBehaviour
{
    [SerializeField] GameObject[] uisToHide;
    [SerializeField] GameObject uiToShow;
    void Update()
    {
        if (Input.anyKeyDown)
        {
            foreach(GameObject ui in uisToHide)
            {
                ui.SetActive(false);
            }
            uiToShow.SetActive(true);
        }
        
    }
}
