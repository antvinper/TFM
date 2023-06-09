using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterModel
{
    public void TakeDamage(StatModificator statModificator);
    public void TakeRealDamage(StatModificator statModificator);
    public void Heal(StatModificator statModificator);

}
