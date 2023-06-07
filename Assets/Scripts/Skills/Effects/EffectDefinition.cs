using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class EffectDefinition : ScriptableObject
{
    [SerializeField] protected new string name;
    [SerializeField] [TextArea] protected string description;


    public abstract Task ProcessEffect(Characters.CharacterController owner, Characters.CharacterController target);

}