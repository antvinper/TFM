using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] protected int price;
    protected int finalPrice;
    [SerializeField] protected Sprite sprite;

    [SerializeField] protected new string name;
    [SerializeField] protected string description;

    protected string nameSufix;

    public string Name
    {
        get => name;
        set => name = value;
    }
    public string Description
    {
        get => description;
    }

    public int FinalPrice
    {
        get => finalPrice;
    }

    public string NameSufix
    {
        get => nameSufix;
    }

    public Sprite Sprite
    {
        get => sprite;
    }
}
