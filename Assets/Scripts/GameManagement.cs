using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    [Header("FileSelection")]
    [SerializeField] public int selectedFile;

    [Header("CurrentRoom")]
    [SerializeField] int roomNumber;
    [SerializeField] int timeSpent;
    [SerializeField] int newCoins;
    [SerializeField] int newSouls;
    [SerializeField] bool inCombat;
    private float timer = 0f;

    [Header("OverallProgress")]
    [SerializeField] int totalSouls;
    [SerializeField] int totalTime;
    [SerializeField] int finishedRuns;

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

    public void SetUpSaveFile(int saveFileNum)
    {
        selectedFile = saveFileNum;
    }

    public void LoadSaveFile()
    {
        // Load the variables needed for the game to run, depending on which file is selected
        // selectedFile
    }

    public void EndRoom()
    {
        totalTime += timeSpent;
        timeSpent = 0;

        totalSouls += newSouls;

        inCombat = true;
        roomNumber += 1;
    }
}
