using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoomModel
{
    [SerializeField] private RoomTypesEnum roomType;
    [SerializeField] private List<GameObject> enemiesPrefabs;
    [SerializeField] private int rupeesMinAmount;
    [SerializeField] private int rupeesMaxAmount;

    public int GetRupeesAmount()
    {
        return Random.Range(rupeesMinAmount, rupeesMaxAmount+1);
    }
}
