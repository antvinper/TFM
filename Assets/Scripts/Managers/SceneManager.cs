using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager: SingletonMonoBehaviour<SceneManager>
{
    private List<int> availableScenes;    
    private int numRooms = 14;
    private int countRooms;

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
        countRooms = GameManager.Instance.RunLevel;
        if (availableScenes.Count > 0)
        {
            if (countRooms == 7 || countRooms == 13 || countRooms == 14)
            {
                switch (countRooms)
                {
                    case 7:
                        LoadScene("Improvement1RoomLevel1");
                        break;

                    case 13:
                        LoadScene("ObjectStore");
                        break;

                    case 14:
                        LoadScene("HouseHall14Level1");
                        break;
                }
            }
            else
            {
                //Get random index
                int randomIndex = Random.Range(0, availableScenes.Count-1);
                int sceneToLoad = availableScenes[randomIndex];
                availableScenes.RemoveAt(randomIndex);
                UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad);
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

    public async Task LoadMenuScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("");
    }

    public async Task LoadLobbyScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("LevelSelector");
    }

    public async Task LoadScene(string scene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scene);
    }

    public string GetActiveScene()
    {
        return UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
    }
}
