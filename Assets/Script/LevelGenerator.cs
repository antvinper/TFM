using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator levelGenerator;
    private List<int> availableScenes;    
    private int numRooms = 12;
    private int countRooms = 1;

    private void Awake()
    {
        if (levelGenerator == null)
        {
            levelGenerator = this;
            DontDestroyOnLoad(gameObject);
        } else if (levelGenerator != this)
        {
            Destroy(gameObject);
        }
        
    }

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
        countRooms++;
        Debug.Log(countRooms);
        if (availableScenes.Count > 0)
        {
            if (countRooms == 7 || countRooms == 13 || countRooms == 14)
            {
                switch (countRooms)
                {
                    case 7:
                        SceneManager.LoadScene("Improvement1RoomLevel1");
                        break;

                    case 13:
                        SceneManager.LoadScene("ObjectStore");
                        break;

                    case 14:
                        SceneManager.LoadScene("HouseHall14Level1");
                        break;
                }
            }
            else
            {
                //Get random index
                int randomIndex = Random.Range(1, availableScenes.Count);
                int sceneToLoad = availableScenes[randomIndex];
                availableScenes.RemoveAt(randomIndex);
                SceneManager.LoadScene(sceneToLoad);
            }
                       
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
