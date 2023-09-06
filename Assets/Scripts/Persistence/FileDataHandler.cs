using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class FileDataHandler
{
    private string dataDirPath = "";
    private string fileName = "";

    private GameModel gameModel;

    public FileDataHandler(string dataDirPath, string fileName)
    {
        this.dataDirPath = dataDirPath;
        this.fileName = fileName;
    }

    public void SaveGame(GameModel data)
    {
        //string fullFileName = fileName + "_" + data.SlotIndex + ".game";
        string fullPath = dataDirPath + "/" + fileName + "_" + data.SlotIndex + ".game";

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

            //VersionConverter converter = new VersionConverter();
            string dataToStore = JsonConvert.SerializeObject(data, settings);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error ocurred when trying to save data to file " + fullPath + "\n" + e);
        }
    }

    public async Task<GameModel> GetDataByFileName(string fileName)
    {
        string fullPath = Path.Combine(dataDirPath, fileName);
        //string fullPath = dataDirPath + "/" + fileName + ".game";
        //GameModel loadedData = null;

        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
                gameModel = JsonConvert.DeserializeObject<GameModel>(dataToLoad, settings);
            }
            catch (Exception e)
            {
                Debug.LogError("Error ocurred when trying to load data from file: " + fullPath + "\n" + e);
                Debug.LogException(e);
            }
        }

        return gameModel;
    }

    public GameMinModel GetMinDataFromSavedFile(string fileName)
    {
        string fullPath = dataDirPath + "/" + fileName;// + ".game";
        GameMinModel minData = null;

        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                minData = JsonConvert.DeserializeObject<GameMinModel>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Error ocurred when trying to load data from file: " + fullPath + "\n" + e);
                Debug.LogException(e);
            }
        }

        return minData;
    }

    public List<string> GetAllFilesForLoad()
    {
        List<string> saveFileNames = new List<string>();

        var info = new DirectoryInfo(dataDirPath);
        List<FileInfo> fileInfo = new List<FileInfo>(info.GetFiles());

        IEnumerable<FileInfo> filoInfoFiltered = fileInfo.Where(x => x.Extension.Equals(".game"));

        foreach (FileInfo f in filoInfoFiltered)
        {
            saveFileNames.Add(f.Name);
        }

        return saveFileNames;
    }
}
