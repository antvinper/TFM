using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class TimeEffectDefinition : EffectDefinition
{
    //protected Characters.CharacterController owner, target;
    /**
     * Necesito una lista de efectos:
     * - Slow down (dismiunye velocidad)
     * - Poison (disminuye la vida poco a poco)
     * - Hurry up (aumenta velocidad)
     */
    [SerializeField] protected EffectTypes effectType;
    [Range(0f, float.MaxValue)]
    [SerializeField] protected float effectLifeTime;

    [HideInInspector] public EffectTypes EffectType { get => effectType; }
    [HideInInspector] public float EffectTime { get => effectLifeTime; }
    //[HideInInspector] public float effectValue { get => Value; }


    [HideInInspector] protected bool reset = false;
    public void Reset()
    {
        Debug.Log(name);
        reset = true;
    }
    [HideInInspector] protected bool cancel = false;
    //public abstract void Cancel();


}
