using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{
    private List<int> availableScenes;
    private int numRooms = 12;
    private int storeSceneIndex = 13;
    private int bossSceneIndex = 14;
    private int countRooms = 0;

    private void Start()
    {
        //List to store the index of available scenes
        availableScenes = new List<int>();

        for (int i = 1; i < numRooms; i++)
        {
            availableScenes.Add(i);
        }

    }

    public void ChangeToRandomScene()
    {
        countRooms = 1;
        if (availableScenes.Count > 0)
        {
            if (countRooms == 7)
            {
                SceneManager.LoadScene("Improvement1RoomLevel1");
            }

            if (countRooms == 13)
            {
                SceneManager.LoadScene("ObjectStore");
            }

            if (countRooms == 14)
            {
                SceneManager.LoadScene("HouseHall14Level1");
            }

            //Get random index
            int randomIndex = Random.Range(1, availableScenes.Count);
            int sceneToLoad = availableScenes[randomIndex];
            availableScenes.RemoveAt(randomIndex);
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            //If all scenes have been visited, we reset the list to play again.
            for (int i = 1; i < numRooms; i++)
            {
                availableScenes.Add(i);
            }
        }
    }
}
