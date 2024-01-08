using CompanyStats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ComboStruct
{
    public SkillDefinition skill;
    public ButtonsXbox button;
    public bool hasDash;
    public float dashTime;
    public float dashPower;
}

public abstract class ComboDefinition : ScriptableObject//, IComboModel
{
    [SerializeField] protected string name;
    public string Name
    {
        get => name;
    }
    [SerializeField] protected string description;
    public string Description => description;

    //[SerializeField] protected float[] damageMultiplier;
    //public float[] DamageMultiplier => damageMultiplier;

    //[SerializeField] protected List<SkillDefinition> skills = new List<SkillDefinition>();
    //public List<SkillDefinition> Skills => skills;

    protected bool isActive;
    public bool IsActive => isActive;
    [SerializeField] protected bool startsActive;
    public bool StartsActive => startsActive;

    //[SerializeField] protected ButtonsXbox[] buttons;
    //public ButtonsXbox[] Buttons => buttons;

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

    [SerializeField] protected AnimationTriggersEnum animationTriggerToStart;
    public AnimationTriggersEnum AnimationTriggerToStart => animationTriggerToStart;

    [SerializeField] protected List<ComboStruct> comboStruct;

}
