using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveGame(GameManagement gameManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/savefile-" + gameManager.selectedFile + ".data";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(gameManager);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SaveData LoadGame(int selectedFile)
    {
        string path = Application.persistentDataPath + "/savefile-" + selectedFile + ".data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("No se encuentran datos de guardado en: " + path);
            return null;
        }
    }
}
