using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [Header("GameManager")]
    [SerializeField] GameManagement gameManager;

    [Header("TextsToChange")]
    [SerializeField] TextMeshProUGUI saveFileText;

    
    void Start()
    {
        saveFileText.text = "Partida " + gameManager.selectedFile;
    }
}
