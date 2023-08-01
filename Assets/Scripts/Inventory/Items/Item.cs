using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] protected int price;
    [SerializeField] protected Sprite sprite;

    protected string name;
    protected string description;

    public string Name
    {
        get => name;
        set => name = value;
    }
    public string Description
    {
        get => description;
    }
    public int Price
    {
        get => price;
    }

    public Sprite Sprite
    {
        get => sprite;
    }
}
