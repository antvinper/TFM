using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : DontDestroyOnLoad where T:MonoBehaviour
{
    private static T instance;
    public static T Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this as T;
        }
    }
}