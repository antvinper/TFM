using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterModel
{
    public StatModificator TakeDamage(StatModificator statModificator);
    public StatModificator TakeRealDamage(StatModificator statModificator);
    public StatModificator Heal(StatModificator statModificator);

}
