using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] roomPrefabs; //List of prefabs for the different rooms
    private List<GameObject> spawnedRooms; //List of room spawned

    private int currentRoom;
    public int numberRooms = 14;

    void Start()
    {
        spawnedRooms = new List<GameObject>();
        currentRoom = 0;

        GenerateLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateLevel()
    {
        for (int i = 0; i < numberRooms; i++)
        {
            GameObject room = Instantiate(roomPrefabs[Random.Range(0, roomPrefabs.Length)]);
            spawnedRooms.Add(room);

            if (i == 0) //Initial room
                room.transform.position = Vector3.zero;
            else if (i != 7 && i != 13)
            {
                //Generate rooms random
                Vector3 spawnPosition = FindValidSpawnPosition(); //Found a valid position to place the next room
                room.transform.position = spawnPosition;
            }

            // Desactiva las salas para mostrar solo la sala actual.
            room.SetActive(i == currentRoom);
        }
    }

    private Vector3 FindValidSpawnPosition()
    {
        Vector3 spawnPosition = spawnedRooms[currentRoom].transform.position;
        int randomDirection = Random.Range(0, 4);

        switch (randomDirection)
        {
            case 0:
                spawnPosition += Vector3.left * 10f;
                break;
            case 1:
                spawnPosition += Vector3.right * 10f;
                break;
            case 2:
                spawnPosition += Vector3.forward * 10f;
                break;
            case 3:
                spawnPosition += Vector3.back * 10f;
                break;
        }

        return spawnPosition;
    }

    public void MoveToNextRoom()
    {
        if (currentRoom >= 0 && currentRoom < spawnedRooms.Count)
        {
            spawnedRooms[currentRoom].SetActive(false);
            currentRoom++;

            if (currentRoom >= spawnedRooms.Count)
            {
                Debug.Log("¡Has llegado al final del laberinto!");
                return;
            }

            spawnedRooms[currentRoom].SetActive(true);
        }
    }
}
