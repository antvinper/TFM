using UnityEngine;

[System.Serializable]
public class SaveData
{
    [Header("OngoingRun")]
    public int roomNumber;  //Contador de salas completadas para definir cu�ndo llega al jefe
    public int levelId;     //ID de la escena en la que est� el jugador
    public int coins;
    public int souls;
    //Stats del jugador o bonuses recogidos y se calculan las stats al cargar partida

    [Header("AllTimeProgress")]
    public int totalTime;
    public int finishedRuns;

    public SaveData(GameManagement gameManager) //A�adir el objeto de tipo stats y cargar los valores al constructor
    {
        roomNumber = gameManager.roomNumber;
        levelId = 0; //TO DO
        coins = gameManager.coins;
        souls = gameManager.totalSouls;
        totalTime = gameManager.totalTime;
        finishedRuns = gameManager.finishedRuns;
    }


    
}
