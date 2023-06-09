using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterModel
{
    public void TakeDamage(StatModificator statModificator);
    public void TakePercentualDamage(StatModificator statModificator);
    public void Heal(float value);

}
