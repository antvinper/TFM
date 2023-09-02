using UnityEngine;

[System.Serializable]
public class SaveData
{
    [Header("OngoingRun")]
    public int roomNumber;  //Contador de salas completadas para definir cuándo llega al jefe
    public int levelId;     //ID de la escena en la que está el jugador
    public int coins;
    public int souls;
    //Stats del jugador o bonuses recogidos y se calculan las stats al cargar partida

    [Header("AllTimeProgress")]
    public int totalTime;
    public int finishedRuns;

    public SaveData(GameManagement gameManager) //Añadir el objeto de tipo stats y cargar los valores al constructor
    {
        roomNumber = gameManager.roomNumber;
        levelId = 0; //TO DO
        coins = gameManager.coins;
        souls = gameManager.totalSouls;
        totalTime = gameManager.totalTime;
        finishedRuns = gameManager.finishedRuns;
    }


    
}
