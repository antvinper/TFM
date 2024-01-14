using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class FileFolder : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI fileNumberText;
    [SerializeField] TextMeshProUGUI gameTimeText;
    [SerializeField] TextMeshProUGUI buttonText;
    private string name = "";
    private bool isNewGame = true;
    public bool IsNewGame { get => isNewGame; }
    private GameModel gameModel;
    private int slotIndex;
    public int SlotIndex { get => slotIndex; }
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
        //Debug.Log(model.TotalPlaytime.ToString("hh':'mm':'ss"));
        gameTimeText.text += model.TotalPlaytime.ToString("hh':'mm':'ss");
    }

    public void FillFileFolderForNewGame(int index)
    {
        slotIndex = index;
        fileNumberText.text = "Partida " + index;
        //Debug.Log("New game folder");
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
}
