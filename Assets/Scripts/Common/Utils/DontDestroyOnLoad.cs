using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    protected void NotDestroy()
    {
        DontDestroyOnLoad(this);
    }
}
