using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatModifier
{
    private IModifyStatBehaviour behaviour;

    public StatModifier(StatModificator statModificator)
    {
        if (statModificator.StatToModify.Equals(StatsEnum.HEALTH))
        {
            behaviour = new ModifyHealthBehaviour();
        }
        else
        {
            SetCommonBehaviour(statModificator.IsPermanent);
        }

    }

    private void SetCommonBehaviour(bool isPermanent)
    {
        if (isPermanent)
        {
            behaviour = new ModifyStatPermanentBehaviour();
        }
        else
        {
            behaviour = new ModifyStatInRunBehaviour();
        }
    }

    public void PerformBehaviour(Characters.CharacterController characterController, StatModificator statModificator)
    {

        behaviour.ExecuteBehaviour(characterController, statModificator);
    }
}
