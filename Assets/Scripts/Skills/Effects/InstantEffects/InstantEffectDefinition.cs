using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public abstract class InstantEffectDefinition: EffectDefinition
{
    /**
     * Tipos de efectos instantáneos:
     * - Ataque
     * - Curación
     * - Modificador de stat
     * 
     * 
     */
    [SerializeField] protected StatsEnum statAffected;
    [SerializeField] protected bool isValueInPercentage;

    [HideInInspector]
    [SerializeField]
    [Range(0, 100)]
    protected int valueInPercentage;

    //TODO??
    //Habrá que saber de qué estado y de quién tenemos que referenciar.
    //Podemos curar en función de un porcentaje de vida del receptor
    //Podemos curar en función del ataque mágico del lanzador
    [HideInInspector]
    [SerializeField]
    [Tooltip("Este será el estado del cual sacar el porcentaje. Por ejemplo: " +
        "queremos hacer un daño porcentual en función de la vida del atacante o del target.")]
    protected StatsEnum statWhatToSee;
    [HideInInspector]
    [SerializeField]
    [Tooltip("Este será de quién mirar ese stat")]
    protected bool isTheOwnerStat;

}
