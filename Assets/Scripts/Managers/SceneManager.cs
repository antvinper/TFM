using UnityEngine;

public class SceneManager : MonoBehaviour
{
    private int numScene;

    private void OnTriggerEnter(Collider other)
    {        
        if (other.tag == "Player")
        {
            numScene = Random.Range(1, 5);

            if (numScene == 1)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("HouseHall1Level1");
            }

            if (numScene == 2)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("HouseHall2Level1");
            }

            if (numScene == 3)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("HouseHall3Level1");
            }

            if (numScene == 4)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("HouseHall4Level1");
            }
        }
    }
}
