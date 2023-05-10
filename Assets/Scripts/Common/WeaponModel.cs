using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponModel : MonoBehaviour
{
    [SerializeField] float Damage;
    [SerializeField] float MaxTimeCharge;
    [SerializeField] float MaxDamageMultiplier;
    [SerializeField] BasicComboDefinition[] BasicComboDefinitions;
    int IndexComboActive = -1;

    public float damage { get => Damage; }
    public float maxTimeCharge { get => MaxTimeCharge; }
    public float maxDamageMultiplier { get => MaxDamageMultiplier; }
    public BasicComboDefinition[] basicComboDefinitions { get => BasicComboDefinitions; }
    public int indexComboActive { get => IndexComboActive; set { IndexComboActive = value; } }
    
}
