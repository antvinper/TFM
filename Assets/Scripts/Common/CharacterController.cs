using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            StatModifier statModifier = new StatModifier(statModificator);
            statModifier.PerformBehaviour(this, statModificator);
        }

        public void ChangeStatPermanent(StatModificator statModificator)
        {
            model.PerformPermanentChangeState(statModificator);
        }

        public bool TryAddTemporallyState(DuringTimeEffect duringTimeEffect)
        {
            bool hasBeenAdded = false;
            /**
             * TENER EN CUENTA EL TIPO NONE. Este no debe influir a la hora de aplicar o no los efectos.
             * Siempre se aplicar� con el tipo NONE
             */
            DuringTimeEffectDefinition te = model.TimeEffectDefinitions.Where(t => t.EffectType.Equals(duringTimeEffect.EffectType)).FirstOrDefault() as DuringTimeEffectDefinition;
            if (te == null)
            {
                model.TimeEffectDefinitions.Add(duringTimeEffect);
                hasBeenAdded = true;
            }
            else
            {
                if (duringTimeEffect.Value.Equals(te.Value) && duringTimeEffect.EffectTime.Equals(te.EffectTime))
                {
                    Debug.Log(te.name + " reset");
                    te.Reset();
                }
                else if (duringTimeEffect.Value > te.Value || duringTimeEffect.EffectTime > te.EffectTime)
                {
                    Debug.Log(te.name + " cancel");
                    te.Cancel();
                    model.TimeEffectDefinitions.Remove(te);
                    model.TimeEffectDefinitions.Add(duringTimeEffect);
                    hasBeenAdded = true;
                }
            }
            return hasBeenAdded;
        }

        public bool TryAddTemporallyState(OverTimeEffect overTimeEffect)
        {
            bool hasBeenAdded = false;

            OverTimeEffectDefinition te = model.TimeEffectDefinitions.Where(t => t.EffectType.Equals(overTimeEffect.EffectType)).FirstOrDefault() as OverTimeEffectDefinition;

            if (te == null)
            {
                model.TimeEffectDefinitions.Add(overTimeEffect);
                hasBeenAdded = true;
            }
            else
            {
                if (overTimeEffect.Value.Equals(te.Value) && overTimeEffect.EffectTime.Equals(te.EffectTime) && overTimeEffect.TimeBetweenApplyEffect.Equals(te.TimeBetweenApplyEffect))
                {
                    te.Reset();
                }
                else if (overTimeEffect.Value > te.Value || overTimeEffect.EffectTime > te.EffectTime || overTimeEffect.TimeBetweenApplyEffect < te.TimeBetweenApplyEffect)
                {
                    te.Cancel();
                    model.TimeEffectDefinitions.Remove(te);
                    model.TimeEffectDefinitions.Add(overTimeEffect);
                    hasBeenAdded = true;
                }
            }

            return hasBeenAdded;
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

        public float GetPermanentStat(StatsEnum stat)
        {
            float value = 0;

            switch (stat)
            {
                case StatsEnum.MAX_HEALTH:
                    value = model.MaxHealthWithPermanent;
                    break;
                case StatsEnum.SPEED:
                    value = model.SpeedWithPermanent;
                    break;
                /**
                 * TODO
                 */
            }

                    return value;
        }

        public float GetStat(StatsEnum stat)
        {
            float value = 0;

            switch (stat)
            {
                case StatsEnum.MAX_HEALTH:
                    value = model.MaxHealth;
                    break;
                case StatsEnum.HEALTH:
                    value = model.Health;
                    break;
                case StatsEnum.SPEED:
                    value = model.Speed;
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

