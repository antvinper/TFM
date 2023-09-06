using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AspectRatio : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;

    void Start()
    {
        dropdown.onValueChanged.AddListener(ChangeResolution);
    }

    public void ChangeResolution(int val)
    {
        switch (val)
        {
            case 0:
                Screen.SetResolution(1920, 1080, false);
                break;
            case 1:
                Screen.SetResolution(1440, 900, false);
                break;
            case 2:
                Screen.SetResolution(1280, 800, false);
                break;
        }
        
    }
}
