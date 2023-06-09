using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateModifier
{
    private IModifyStateBehaviour behaviour;

    public StateModifier(StatModificator statModificator)
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
            behaviour = new ModifyStatePermanentBehaviour();
        }
        else
        {
            behaviour = new ModifyStateTemporallyBehaviour();
        }
    }

    public void PerformBehaviour(Characters.CharacterController characterController, StatModificator statModificator)
    {

        behaviour.ExecuteBehaviour(characterController, statModificator);
    }
}
