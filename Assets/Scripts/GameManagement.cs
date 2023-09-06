using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    [Header("FileSelection")]
    [SerializeField] public int selectedFile;
    [SerializeField] public bool hasOngoingRun;

    [Header("CurrentRun")]
    [SerializeField] public int weaponId;
    [SerializeField] public int roomNumber;
    [SerializeField] public int timeSpent;
    [SerializeField] public int coins;
    [SerializeField] public int newSouls;
    [SerializeField] public bool inCombat;
    private float timer = 0f;

    [Header("OverallProgress")]
    [SerializeField] public int totalSouls;
    [SerializeField] public int totalTime;
    [SerializeField] public int finishedRuns;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (inCombat)
        {
            timer += Time.deltaTime;

            if(timer >= 1.0f)
            {
                timer = 0f;
                timeSpent++;
            }
        }
    }

    public void SetWeapon(int id)
    {
        weaponId = id; //0 para la Espada, 1 para el Chakram
    }

    public void SetUpSaveFile(int saveFileNum)
    {
        selectedFile = saveFileNum;
    }

    public void EndRoom()
    {
        totalTime += timeSpent;
        timeSpent = 0;

        totalSouls += newSouls;
        newSouls = 0;

        inCombat = true;
        roomNumber += 1;
    }

    public void saveFile()
    {
        SaveSystem.SaveGame(this);
    }

    public void LoadSaveFile()
    {
        // Load the variables needed for the game to run, depending on which file is selected
        // selectedFile
        SaveData data = SaveSystem.LoadGame(selectedFile);

        roomNumber = data.roomNumber;
        timeSpent = 0;
        coins = data.coins;
        newSouls = 0;
        inCombat = false;
        totalSouls = data.souls;
        totalTime = data.totalTime;
        finishedRuns = data.finishedRuns;
    }
}
