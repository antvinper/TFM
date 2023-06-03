using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class FileDataHandler : MonoBehaviour
{
    private string dataDirPath = "";
    private string fileName = "";

    public FileDataHandler(string dataDirPath, string fileName)
    {
        this.dataDirPath = dataDirPath;
        this.fileName = fileName;
    }

    public void SaveGame(GameData data)
    {
        string fullFileName = fileName + "_" + data.SlotIndex + ".game";
        string fullPath = Path.Combine(dataDirPath, fullFileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            VersionConverter converter = new VersionConverter();
            string dataToStore = JsonConvert.SerializeObject(data, converter);

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

    public GameData GetDataByFileName(string fileName)
    {
        //string fullPath = Path.Combine(dataDirPath, fileName);
        string fullPath = dataDirPath + "/" +  fileName + ".game";
        GameData loadedData = null;

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

                loadedData = JsonConvert.DeserializeObject<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Error ocurred when trying to load data from file: " + fullPath + "\n" + e);
                Debug.LogException(e);
            }
        }

        return loadedData;
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
