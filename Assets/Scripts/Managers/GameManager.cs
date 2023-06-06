using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private bool isGameStarted = false;
    private DataPersistenceManager dataPersistenceManager;

    public GameModel GameData
    {
        get => dataPersistenceManager.gameData;
    }

    private void Start()
    {
        dataPersistenceManager = new DataPersistenceManager();

        string fileName = "GameName_SaveData_";
        string slot = "0";

        LoadData(fileName + slot);

        

        NewGame();
    }

    public void NewGame()
    {
        dataPersistenceManager.NewGame();
        isGameStarted = true;
    }

    public bool IsGameStarted
    {
        get => this.isGameStarted;
        set => isGameStarted = value;
    }

    public void SaveGame(int slotIndex = 0)
    {
        dataPersistenceManager.SaveGame(slotIndex);
    }

    public void LoadData(string fileName)
    {
        dataPersistenceManager.LoadGame(fileName);
    }

    public GameModel GetDataByFileName(string fileName)
    {
        return dataPersistenceManager.GetDataByFileName(fileName);
    }

    private void OnApplicationQuit()
    {
        if (isGameStarted)
        {
            /**
             * TODO
             * Get the player quaternion to save the data
             */
            SavePlayerLocation();

            SaveGame(0);
        }
    }

    private void SavePlayerLocation()
    {
        SavePlayerPosition();
        SavePlayerRotation();
    }

    private void SavePlayerPosition()
    {
        Vector3 position = PlayerController.Instance.gameObject.transform.position;
        dataPersistenceManager.gameData.PositionX = position.x;
        dataPersistenceManager.gameData.PositionY = position.y;
        dataPersistenceManager.gameData.PositionZ = position.z;
    }

    private void SavePlayerRotation()
    {
        Quaternion rotation = PlayerController.Instance.gameObject.transform.rotation;

        dataPersistenceManager.gameData.RotationX = rotation.x;
        dataPersistenceManager.gameData.RotationY = rotation.y;
        dataPersistenceManager.gameData.RotationZ = rotation.z;
        dataPersistenceManager.gameData.RotationW = rotation.w;
    }
}
