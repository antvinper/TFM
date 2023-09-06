using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FileSelect : MonoBehaviour
{
    private List<FileFolder> fileFolders;
    // Start is called before the first frame update
    void Start()
    {
        fileFolders = new List<FileFolder>(GetComponentsInChildren<FileFolder>());
        FillFileFolders();
    }

    public async Task FillFileFolders()
    {
        await new WaitUntil(() => MainMenuManager.Instance != null);
        
        Debug.Log("Filling file folders");
        List<GameModel>gameModels = await MainMenuManager.Instance.GetAllDataSaved();

        int slotIndex = 0;
        foreach(GameModel gameModel in gameModels)
        {
            fileFolders[gameModel.SlotIndex].FillFileFolderForLoadGame(gameModel);
        }
        for (int i = gameModels.Count; i <= 3; ++i)
        {
            fileFolders[i].FillFileFolderForNewGame(i);
        }

        Debug.Log("HOLA");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
