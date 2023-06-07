using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public abstract class DuringTimeEffect : EffectDefinition
{
    /**
     * Aplica el efecto durante un tiempo determinado.
     * Por ejemplo, puede aplicar una ralentización de 5sg.
     * 
     */
    [SerializeField] protected float reductionValue;
    [SerializeField] protected float effectTime;
    [SerializeField] protected StatsEnum statAffected;
    [SerializeField] protected bool isPositive;
}
