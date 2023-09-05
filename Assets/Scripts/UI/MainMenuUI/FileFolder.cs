using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class FileFolder : MonoBehaviour
{
    //[SerializeField] int fileNum;
    [SerializeField] TextMeshProUGUI fileNumberText;
    [SerializeField] TextMeshProUGUI gameTimeText;
    [SerializeField] TextMeshProUGUI buttonText;
    private string name = "";
    private bool isNewGame = true;
    private GameModel gameModel;
    private int slotIndex;
    public string Name
    {
        get => name;
        set => name = value;
    }

    public void FillFileFolderForLoadGame(GameModel model)
    {
        this.gameModel = model;
        isNewGame = false;
        fileNumberText.text = "Partida " + model.SlotIndex;
        Debug.Log(model.TotalPlaytime.ToString("hh':'mm':'ss"));
        gameTimeText.text += model.TotalPlaytime.ToString("hh':'mm':'ss");
    }

    public void FillFileFolderForNewGame(int index)
    {
        slotIndex = index;
        fileNumberText.text = "Partida " + index;
        Debug.Log("New game folder");
        gameTimeText.text = "";
        buttonText.text = "New Game";
    }

    public async Task StartGame()
    {
        if (isNewGame)
        {
            GameManager.Instance.NewGame(slotIndex);
        }
        else
        {
            GameManager.Instance.LoadGame(gameModel);
        }
        
    }

    //[SerializeField] GameManagement gameManager;


    /*void Start()
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
        //gameManager.selectedFile = fileNum;
    }*/
}
