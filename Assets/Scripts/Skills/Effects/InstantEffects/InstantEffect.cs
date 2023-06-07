using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public abstract class InstantEffect: EffectDefinition
{
    /**
     * Tipos de efectos instant�neos:
     * - Ataque
     * - Curaci�n
     * - Modificador de stat
     * 
     * (herencia)
     * 
     */
    public EffectType effectType;


    /*public abstract Task ProcessEffect(Characters.CharacterController owner, Characters.CharacterController target);*/
}
