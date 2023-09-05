using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FileSelect : MonoBehaviour
{
    [SerializeField] int fileNum;
    [SerializeField] TextMeshProUGUI fileNumberText;
    [SerializeField] TextMeshProUGUI gameTimeText;
    [SerializeField] TextMeshProUGUI buttonText;

    [SerializeField] GameManagement gameManager;


    void Start()
    {
        switch (fileNum)
        {
            case 1:
                fileNumberText.text = "Partida 1";
                gameTimeText.text = "Tiempo de juego:" + Environment.NewLine + "7 horas 30 minutos";
                buttonText.text = "[ Continuar ]";
                break;
            case 2:
                fileNumberText.text = "Partida 2";
                gameTimeText.text = "Tiempo de juego:" + Environment.NewLine + " 2 horas 10 minutos";
                buttonText.text = "[ Continuar ]";
                break;
            case 3:
                fileNumberText.text = "Partida 3";
                gameTimeText.text = "Sin datos de guardado";
                buttonText.text = "[ Nueva partida ]";
                break;
        }
        
    }

    public void SelectFile()
    {
        gameManager.selectedFile = fileNum;
    }
}
