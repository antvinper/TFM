using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public abstract class InstantEffectDefinition: EffectDefinition
{
    /**
     * Tipos de efectos instant�neos:
     * - Ataque
     * - Curaci�n
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
    //Habr� que saber de qu� estado y de qui�n tenemos que referenciar.
    //Podemos curar en funci�n de un porcentaje de vida del receptor
    //Podemos curar en funci�n del ataque m�gico del lanzador
    [HideInInspector]
    [SerializeField]
    [Tooltip("Este ser� el estado del cual sacar el porcentaje. Por ejemplo: " +
        "queremos hacer un da�o porcentual en funci�n de la vida del atacante o del target.")]
    protected StatsEnum statWhatToSee;
    [HideInInspector]
    [SerializeField]
    [Tooltip("Este ser� de qui�n mirar ese stat")]
    protected bool isTheOwnerStat;

}
