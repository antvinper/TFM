using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DataPersistenceManager// : Singleton<DataPersistenceManager>
{
    public string fileName;
    public GameModel gameData;
    private FileDataHandler dataHandler;

    public DataPersistenceManager()
    {
        fileName = "GameName_SaveData";
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
    }
    // Start is called before the first frame update
    /*void Start()
    {
        fileName = "GameName_SaveData";
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
    }*/

    public void NewGame()
    {
        this.gameData = new GameModel();
        this.gameData.Setup();

        /*
         * TODO
         * Iniciar Cuando se empiece juego o se cargue partida
         *
         */
        this.gameData.startSessionTime = System.DateTime.Now;
    }

    public void SaveGame(int slotIndex = 0)
    {
        System.TimeSpan t = this.gameData.GetTotalPlayTime();
        gameData.SlotIndex = slotIndex;
        dataHandler.SaveGame(gameData);
    }

    public async Task LoadGame(string fileName)
    {
        this.gameData = await GetDataByFileName(fileName);
    }

    /*
     * Call this if we have more than one slot
     */
    public List<string> GetAllFilesForLoad()
    {
        return dataHandler.GetAllFilesForLoad();
    }


    public async Task<GameModel> GetDataByFileName(string fileName)
    {
        return await dataHandler.GetDataByFileName(fileName);
    }

    public GameMinModel GetMinDataByFileName(string fileName)
    {
        return dataHandler.GetMinDataFromSavedFile(fileName);
    }
}
