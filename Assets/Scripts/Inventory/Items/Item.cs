using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] protected int price;
    [SerializeField] protected Sprite sprite;

    public int Price
    {
        get => price;
    }

    public Sprite Sprite
    {
        get => sprite;
    }
}
