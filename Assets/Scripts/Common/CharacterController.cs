using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters
{
    public abstract class CharacterController : MonoBehaviour
    {
        [SerializeField] protected CharacterMutableModel model;
        public abstract void ProcessDamage(float value);

        public abstract float GetMyRealDamage();


        public void ChangeStat(StatModificator statModificator)
        {
            StateModifier stateModifier = new StateModifier(statModificator);
            stateModifier.PerformBehaviour(this, statModificator);
        }

        public void ChangeStatPermanent(StatModificator statModificator)
        {
            model.PerformPermanentChangeState(statModificator);
        }

        public bool TryAddTemporallyState(TimeEffectDefinition timeEffectDefinition)
        {
            return model.TryAddTemporallyState(timeEffectDefinition);
        }

        public void ChangeStatTemporally(StatModificator statModificator)
        {
            model.PerformTemporallyState(statModificator);
        }

        public void ChangeRealHealth(StatModificator statModificator)
        {
            model.PerformRealHealthChange(statModificator);
        }
        public void ChangePercentualHealth(StatModificator statModificator)
        {
            model.PerformPercentualHealthChange(statModificator);
        }

        public float GetStat(StatsEnum stat)
        {
            float value = 0;

            switch (stat)
            {
                case StatsEnum.SPEED:
                    value = model.Speed;
                    break;
                case StatsEnum.MAX_HEALTH:
                    value = model.MaxHealth;
                    break;
                case StatsEnum.ATTACK:
                    value = model.Attack;
                    break;
                /**
                * TODO
                */
            }

            return value;
        }
    }
}

