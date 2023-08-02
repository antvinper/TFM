using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ComboDefinition : ScriptableObject//, IComboModel
{
    [SerializeField] protected string name;
    public string Name
    {
        get => name;
    }
    [SerializeField] protected string description;
    public string Description => description;

    [SerializeField] protected float[] damageMultiplier;
    public float[] DamageMultiplier => damageMultiplier;

    [SerializeField] protected bool isActive;
    public bool IsActive => isActive;

    [SerializeField] protected ButtonsXbox[] buttons;
    public ButtonsXbox[] Buttons => buttons;

    [SerializeField] protected bool comboFinished = false;
    public bool ComboFinished
    {
        get => comboFinished;
        set => comboFinished = value;
    }

    [SerializeField] protected int price;
    public int Price => price;

    [SerializeField] protected Sprite sprite;
    public Sprite Sprite => sprite;



}
