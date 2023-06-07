using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public abstract class OverTimeEffect : EffectDefinition
{
    /**
     * Aplica el efecto cada x tiempo.
     * Por ejemplo. Si es tipo veneno, causará daño cada x tiempo
     * Si es tipo curación, se irá curando x cantidad cada cierto tiempo.
     */
}
