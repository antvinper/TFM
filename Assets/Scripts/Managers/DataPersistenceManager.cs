using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DataPersistenceManager// : Singleton<DataPersistenceManager>
{
    public string fileName;
    public GameModel gameModel;
    private FileDataHandler dataHandler;
    private bool isDataLoaded = false;
    
    public bool IsDataLoaded { get => isDataLoaded; }
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

    public void NewGame(int slotIndex)
    {
        this.gameModel = new GameModel();
        this.gameModel.Setup(slotIndex);

        /*
         * TODO
         * Iniciar Cuando se empiece juego o se cargue partida
         *
         */
        this.gameModel.startSessionTime = System.DateTime.Now;
    }

    public void SaveGame()
    {
        System.TimeSpan t = this.gameModel.GetTotalPlayTime();
        //gameModel.SlotIndex = slotIndex;
        dataHandler.SaveGame(gameModel);
    }

    public async Task LoadGame(string fileName)
    {
        this.gameModel = await GetDataByFileName(fileName);
        if(this.gameModel != null)
        {
            isDataLoaded = true;
        }
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
