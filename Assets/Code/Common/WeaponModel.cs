using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponModel : MonoBehaviour
{
    [SerializeField] private float Damage;
    [SerializeField] private float MaxTimeCharge;
    [SerializeField] private float MaxDamageMultiplier;

    public float damage { get => Damage; }
    public float maxTimeCharge { get => MaxTimeCharge; }
    public float maxDamageMultiplier { get => MaxDamageMultiplier; }
    
}
