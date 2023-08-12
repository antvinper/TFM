using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private bool isGameStarted = false;
    private bool isGameInRun = false;
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
    public PlayerController GetPlayerController()
    {
        return this.playerController;
    }

    private void Start()
    {
        dataPersistenceManager = new DataPersistenceManager();


        LoadData();
        //NewGame();
    }


    public void NewGame()
    {
        
        dataPersistenceManager.NewGame();
        isGameStarted = true;

        //SceneManager.Instance.LoadLobbyScene();
    }

    public bool IsGameStarted
    {
        get => this.isGameStarted;
        set => isGameStarted = value;
    }
    public bool IsGameInRun
    {
        get => isGameInRun;
        set => isGameInRun = value;
    }

    public void SaveGame(int slotIndex = 0)
    {
        dataPersistenceManager.gameData.PlayerModel = playerController.Model as PlayerMutableModel;
        dataPersistenceManager.SaveGame(slotIndex);
    }

    public void LoadData()
    {
        string fileName = "GameName_SaveData_";
        string slot = "0";
        isGameStarted = true;
        LoadData(fileName + slot);
    }

    public void LoadData(string fileName)
    {
        //dataPersistenceManager = new DataPersistenceManager();

        dataPersistenceManager.LoadGame(fileName);
    }

    public async Task<GameModel> GetDataByFileName(string fileName)
    {
        return await dataPersistenceManager.GetDataByFileName(fileName);
    }

    public List<GameMinModel> GetAllFilesForLoad()
    {
        List<string> filesNames = new List<string>(dataPersistenceManager.GetAllFilesForLoad());
        List<GameMinModel> gameModelsSaved = new List<GameMinModel>();

        foreach(string fileName in filesNames)
        {
            gameModelsSaved.Add(dataPersistenceManager.GetMinDataByFileName(fileName));
        }

        return gameModelsSaved;
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
