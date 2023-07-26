using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{

    private List<int> availableScenes;
    private int numRooms = 14;
    private int storeSceneIndex = 6;
    private int bossSceneIndex = 13;

    private void Start()
    {
        //List to store the index of available scenes
        availableScenes = = new List<int>();

        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            availableScenes.Add(i);
        }

    }

    public void ChangeToRandomScene()
    {
        if (availableScenes.Count > 0)
        {
            //Get random index
            int randomIndex = Random.Range(0, availableScenes.Count);
            int sceneToLoad = availableScenes[randomIndex];
            availableScenes.RemoveAt(randomIndex);

            if (sceneToLoad == storeSceneIndex || sceneToLoad == bossSceneIndex)
            {
                SceneManager.LoadScene(sceneToLoad);
            }
            else
            {
                // Cargamos la escena aleatoria
                SceneManager.LoadScene(sceneToLoad);
            }
        }
        else
        {
            //If all scenes have been visited, we reset the list to play again.
            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                availableScenes.Add(i);
            }
        }
    }
}
