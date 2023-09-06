using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
{
    private static T instance;

    public static T Instance
    {
        get
        {
            CreateInstance();
            return instance;
        }
    }

    public static void CreateInstance()
    {
        if (instance == null)
        {
            //find existing instance
            instance = FindObjectOfType<T>();
            if (instance == null)
            {
                //create new instance
                var go = new GameObject(typeof(T).Name);
                instance = go.AddComponent<T>();
            }
            //initialize instance if necessary
            if (!instance.initialized)
            {
                instance.Initialize();
                instance.initialized = true;
            }
        }
    }

    public virtual void Awake()
    {
        if (Application.isPlaying)
        {
            DontDestroyOnLoad(this);
        }

        //check if instance already exists when reloading original scene
        if (instance != null)
        {
            DestroyImmediate(gameObject);
        }
    }

    protected bool initialized;

    protected virtual void Initialize() { }
}
