using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    [SerializeField] RoomModel model;

    private void Start()
    {
        RoomManager.Instance.RoomController = this; 
    }

    public int GetRupeesAmount()
    {
        return model.GetRupeesAmount();
    }
}
