using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterModel
{
    public bool TakeDamage(float value);
    public bool TakePercentualDamage(float value);
    public void Heal(float value);

}
