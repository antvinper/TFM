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

    private SceneRoot sceneRoot;

    private void Start()
    {
        //List to store the index of available scenes
        availableScenes = new List<int>();

        for (int i = 2; i <= numRooms; i++)
        {
            availableScenes.Add(i);
        }
    }

    internal void Setup(SceneRoot sceneRoot)
    {
        this.sceneRoot = sceneRoot;
    }

    public async Task ChangeToRandomScene()
    {
        countRooms = GameManager.Instance.RunLevel;
        
        if (availableScenes.Count > 0)
        {
            if(countRooms == 2 && GameManager.Instance.isShortGame)
            {
                countRooms = 14;
            }
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

                if (HasToChooseReward(sceneToLoad))
                {
                    GameManager.Instance.GetPlayerController().DeActivateControls(false);
                    await GameManager.Instance.InGameHUD.ChooseRewardAsync();
                    Debug.Log("#REWARD Reward selected!!! Now I can load the next scene");
                }
                else
                {
                    Debug.Log("#REWARD Not reward to selected :(    Now I can load the next scene");
                }

                string sceneName = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(sceneToLoad)); 
                availableScenes.RemoveAt(randomIndex);

                LoadSceneAsyncByName(sceneName);
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

    private bool HasToChooseReward(int sceneIndex)
    {
        return sceneIndex <= 11;
    }

    private async Task LoadSceneAsyncByName(string sceneName)
    {
        await sceneRoot.FadeIn();
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
    }

    public async Task LoadMenuScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("");
    }

    public async Task LoadLobbyScene()
    {
        //UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("LevelSelector");
        LoadSceneAsyncByName("LevelSelector");
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
