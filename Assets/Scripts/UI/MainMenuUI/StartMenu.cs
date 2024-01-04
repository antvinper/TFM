using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [Header("GameManager")]
    [SerializeField] private GameManagement gameManager;

    [Header("TextsToChange")]
    [SerializeField] private TextMeshProUGUI saveFileText;

    [Header("UiElements")]
    [SerializeField] private Button continueButton;
    [SerializeField] private GameObject optionsMenu;

    
    void Start()
    {
        //gameManager.selectedFile = 1;
        //saveFileText.text = "Partida " + gameManager.selectedFile;
        //continueButton.interactable = gameManager.hasOngoingRun;
    }

    public void ContinueRun()
    {
        Debug.Log("Continuando la run del archivo de guardado");
    }
    
    public void NewRun()
    {
        Debug.Log("Comenzando una nueva run en el archivo de guardado");
    }

    public void OpenOptions()
    {
        gameObject.SetActive(false);
        optionsMenu.SetActive(true);
    }
}
