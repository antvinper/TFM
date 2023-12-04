using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    [SerializeField] private List<GameObject> enemies;
    //[SerializeField] private int numberOfEnemiesToSpawn;
    private int enemiesKilled = 0;

    public int EnemiesKilled
    {
        get => enemiesKilled;
    }

    public void RestartEnemiesKilled()
    {
        enemiesKilled = 0;
    }

    public void AddEnemyKilled()
    {
        ++enemiesKilled;
    }

    public List<GameObject> Enemies
    {
        get => enemies;
    }

    /*public int NumberOfEnemiesToSpawn
    {
        get => numberOfEnemiesToSpawn;
    }*/
}
