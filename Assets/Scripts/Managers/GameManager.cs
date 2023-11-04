using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    private bool isGameStarted = false;
    private bool isGameInRun = false;
    private DataPersistenceManager dataPersistenceManager;

    /**
     * TODO
     * Quiz�s borrar. Ahora se usa en el application quit para almacenar la localizaci�n del personaje
     * Pero eso en un futuro no debe ser as� sino s�lo cuando se est� en el lobby.
     */
    private PlayerController playerController;
    

    public GameModel GameModel
    {
        get => dataPersistenceManager.gameModel;
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


        //LoadData();
        //NewGame();
    }


    public void NewGame(int slotIndex)
    {
        dataPersistenceManager.NewGame(slotIndex);
        isGameStarted = true;
        SceneManager.Instance.LoadLobbyScene();
    }

    public bool IsGameStarted
    {
        get => this.isGameStarted;
        set => isGameStarted = value;
    }

    internal void LoadGame(GameModel gameModel)
    {
        dataPersistenceManager.gameModel = gameModel;
        Debug.Log("Game loaded in slot " + dataPersistenceManager.gameModel.SlotIndex + ", now should be change scene");
    }

    public async Task StartRun()
    {
        Debug.Log("Starting run...");
        //SceneManager.Instance.ChangeToRandomScene();
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("HouseHall5Level1");
    }

    public bool IsGameInRun
    {
        get => isGameInRun;
        set => isGameInRun = value;
    }

    public void SaveGame()
    {
        if(dataPersistenceManager.gameModel.PlayerModel != null && playerController != null)
        {
            dataPersistenceManager.gameModel.PlayerModel = playerController.Model as PlayerMutableModel;
            dataPersistenceManager.SaveGame();
        }
        else
        {
            Debug.Log("Couldn't be saved in slot " + dataPersistenceManager.gameModel.SlotIndex + " because, player doesn't exists");
        }
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

    public async Task<List<GameModel>> GetAllFilesForLoad()
    {
        List<string> filesNames = new List<string>(dataPersistenceManager.GetAllFilesForLoad());
        List<GameModel> gameModelsSaved = new List<GameModel>();

        foreach(string fileName in filesNames)
        {
            GameModel model = await dataPersistenceManager.GetDataByFileName(fileName);
            gameModelsSaved.Add(model);
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
            //SavePlayerLocation();
            Debug.Log("Here should save when application quit");
            SaveGame();
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
        dataPersistenceManager.gameModel.PositionX = position.x;
        dataPersistenceManager.gameModel.PositionY = position.y;
        dataPersistenceManager.gameModel.PositionZ = position.z;
    }

    private void SavePlayerRotation()
    {
        Quaternion rotation = playerController.gameObject.transform.rotation;

        dataPersistenceManager.gameModel.RotationX = rotation.x;
        dataPersistenceManager.gameModel.RotationY = rotation.y;
        dataPersistenceManager.gameModel.RotationZ = rotation.z;
        dataPersistenceManager.gameModel.RotationW = rotation.w;
    }
}
