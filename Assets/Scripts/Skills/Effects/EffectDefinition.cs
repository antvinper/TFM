using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class EffectDefinition : ScriptableObject
{
    public new string name;
    [TextArea] public new string description;
    public abstract Task ProcessEffect(Characters.CharacterController owner, Characters.CharacterController target);

}
