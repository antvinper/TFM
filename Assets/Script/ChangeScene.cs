using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private LevelGenerator sceneLoader;
    
    private GameObject sceneLoaderObject;
    
    private string sceneLoaderTag = "SceneLoader";
    
    private void Start()
    {
        //sceneLoader = GetComponentInParent<LevelGenerator>();
        
        sceneLoaderObject = GameObject.FindGameObjectWithTag(sceneLoaderTag);
        if (sceneLoaderObject != null)
        {
    
            sceneLoader = sceneLoaderObject.GetComponent<LevelGenerator>();
        }
        else
        {
            Debug.LogError("No se encontro el GameObject con la etiqueta SceneController. Asegurate de que el GameObject SceneController esta presente en la escena.");
        }
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Change to random scene
            sceneLoader.ChangeToRandomScene();
        }
    }
}
