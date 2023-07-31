using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion: MonoBehaviour
{
    [SerializeField] InstantEffectTemporally effect;

    public void UseItem(Characters.CharacterController target)
    {
        effect.ProcessEffect(target);
    }
}
