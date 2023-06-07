using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private bool isGameStarted = false;
    private DataPersistenceManager dataPersistenceManager;

    /**
     * TODO
     * Quizás borrar. Ahora se usa en el application quit para almacenar la localización del personaje
     * Pero eso en un futuro no debe ser así sino sólo cuando se esté en el lobby.
     */
    private PlayerController playerController;

    public GameModel GameData
    {
        get => dataPersistenceManager.gameData;
    }

    public void SetPlayerController(PlayerController playerController)
    {
        this.playerController = playerController;
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
        Vector3 position = playerController.gameObject.transform.position;
        dataPersistenceManager.gameData.PositionX = position.x;
        dataPersistenceManager.gameData.PositionY = position.y;
        dataPersistenceManager.gameData.PositionZ = position.z;
    }

    private void SavePlayerRotation()
    {
        Quaternion rotation = playerController.gameObject.transform.rotation;

        dataPersistenceManager.gameData.RotationX = rotation.x;
        dataPersistenceManager.gameData.RotationY = rotation.y;
        dataPersistenceManager.gameData.RotationZ = rotation.z;
        dataPersistenceManager.gameData.RotationW = rotation.w;
    }
}
