using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * TODO
 * 
 * Al arma o al personaje?
 * De momento al personaje para las pruebas
 * 
 */

[Serializable]
public class SkillTest
{
    public List<InstantEffect> instantEffects = new List<InstantEffect>();
    public List<OverTimeEffect> overTimeEffects = new List<OverTimeEffect>();
    public List<DuringTimeEffect> duringTimeEffects = new List<DuringTimeEffect>();

}
