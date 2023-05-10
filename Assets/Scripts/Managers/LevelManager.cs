using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private int numScene;

    private void OnTriggerEnter(Collider other)
    {        
        if (other.tag == "Player")
        {
            numScene = Random.Range(1, 5);

            if (numScene == 1)
            {
                SceneManager.LoadScene("HouseHall1Level1");
            }

            if (numScene == 2)
            {
                SceneManager.LoadScene("HouseHall2Level1");
            }

            if (numScene == 3)
            {
                SceneManager.LoadScene("HouseHall3Level1");
            }

            if (numScene == 4)
            {
                SceneManager.LoadScene("HouseHall4Level1");
            }
        }
    }
}
