using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponModel// : MonoBehaviour
{
    [SerializeField] string name;
    [SerializeField] float damage;
    [SerializeField] float maxTimeCharge;
    [SerializeField] float maxDamageMultiplier;
    [SerializeField] BasicComboDefinition[] basicComboDefinitions;
    int indexComboActive = -1;
    

    public float Damage { get => damage; }
    public float MaxTimeCharge { get => maxTimeCharge; }
    public float MaxDamageMultiplier { get => maxDamageMultiplier; }
    public BasicComboDefinition[] BasicComboDefinitions { get => basicComboDefinitions; }
    public int IndexComboActive { get => indexComboActive; set { indexComboActive = value; } }
    
}
