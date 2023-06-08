using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class TimeEffectDefinition : EffectDefinition
{
    [SerializeField] protected float effectTime;
    [SerializeField] protected StatsEnum statAffected;
    [SerializeField] protected bool isPositive;

    [SerializeField] protected bool isValueInPercentage;

    [HideInInspector]
    [Tooltip("Este valor va entre 0 y 100 siendo esto un porcentaje")]
    [SerializeField] [Range(0, 100)] protected int valueInPercentage;

    [HideInInspector]
    [Tooltip("Este valor se usará tal cual.")]
    [SerializeField] protected float value;

}
