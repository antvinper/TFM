using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class TimeEffectDefinition : EffectDefinition
{
    /**
     * Necesito una lista de efectos:
     * - Slow down (dismiunye velocidad)
     * - Poison (disminuye la vida poco a poco)
     * - Hurry up (aumenta velocidad)
     * 
     * Se tiene que guardar una lista de stat modificators en el mutablemodel
     * A partir de ahí calcular los stats, cuando se remueva de la lista el que termine
     * se actualizará automáticamente el valor del stat.
     */
    [SerializeField] protected BuffDebuffTypes buffDebuffType;
    [Range(0f, float.MaxValue)]
    [SerializeField] protected float effectTime;
    [SerializeField] protected StatsEnum statAffected;
    [SerializeField] protected bool isPositive;

    [SerializeField] protected bool isValueInPercentage;

    [HideInInspector]
    [Tooltip("Este valor va entre 0 y 100 siendo esto un porcentaje")]
    [SerializeField] [Range(0, 100)] protected int valueInPercentage;

    [HideInInspector]
    [Tooltip("Este valor se usará tal cual.")]
    [Range(0f, float.MaxValue)]
    [SerializeField] 
    protected float value;

    [HideInInspector] public BuffDebuffTypes BuffDebuffTypes { get => buffDebuffType; }
    [HideInInspector] public float EffectTime { get => effectTime; }
    [HideInInspector] public float Value { get => value; }


    [HideInInspector] protected bool reset = false;
    public void Reset()
    {
        Debug.Log(name);
        reset = true;
    }
    [HideInInspector] protected bool cancel = false;
    public abstract void Cancel();

}
