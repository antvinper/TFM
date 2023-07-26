using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public RandomSceneLoader sceneLoader;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Change to random scene
            sceneLoader.ChangeToRandomScene();
        }
    }
}
