using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressAnything : MonoBehaviour
{
    [SerializeField] GameObject uiToHide;
    [SerializeField] GameObject uiToShow;
    void Update()
    {
        if (Input.anyKeyDown)
        {
            uiToHide.SetActive(false);
            uiToShow.SetActive(true);
        }
        
    }
}
