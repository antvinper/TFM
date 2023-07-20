using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Characters
{
    public abstract class CharacterController : MonoBehaviour
    {
        [SerializeField] protected CharacterMutableModel model;
        public abstract void ProcessDamage(int value);

        public abstract float GetMyRealDamage();


        public void ChangeStat(StatModificator statModificator)
        {
            StatModifier statModifier = new StatModifier(statModificator);
            statModifier.PerformBehaviour(this, statModificator);
        }

        //Permanent fuera de la run, se a�ade siempre
        public void ChangeStatPermanent(StatModificator statModificator)
        {
            //model.PerformInstantlyApplyStat(statModificator);
            model.PerformApplyPermanentStat(statModificator);
        }

        //Permanente S�LO dentro de la run, al terminar la run no tinenen efecto, pero se a�ade siempre
        public void ChangeStatInRun(StatModificator statModificator)
        {
            model.PerformApplyStatModifyInRun(statModificator);
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
            string error = "";
            OverTimeEffectDefinition te = model.TimeEffectDefinitions.Where(t => t.EffectType.Equals(overTimeEffect.EffectType)).FirstOrDefault() as OverTimeEffectDefinition;

            if (te == null)
            {
                model.TimeEffectDefinitions.Add(overTimeEffect);
                hasBeenAdded = true;
            }
            else
            {
                if (overTimeEffect.IsValueInPercentage)
                {
                    if(!te.IsValueInPercentage)
                    {
                        SwitchTimeEffect(te, overTimeEffect);
                        hasBeenAdded = true;
                    }
                    else
                    {
                        if (overTimeEffect.ValueInPercentage.Equals(te.ValueInPercentage) && overTimeEffect.EffectTime.Equals(te.EffectTime) && overTimeEffect.TimeBetweenApplyEffect.Equals(te.TimeBetweenApplyEffect))
                        {
                            te.Reset();
                            error = overTimeEffect.name + " it's going to be reseted because it is the same effect";
                        }
                        else if (overTimeEffect.ValueInPercentage > te.ValueInPercentage || overTimeEffect.EffectTime > te.EffectTime || overTimeEffect.TimeBetweenApplyEffect < te.TimeBetweenApplyEffect)
                        {
                            SwitchTimeEffect(te, overTimeEffect);
                            hasBeenAdded = true;

                            error = overTimeEffect.name + " has been added and the previous effect has been removed.";
                        }
                    }
                } else
                {
                    if (overTimeEffect.Value.Equals(te.Value) && overTimeEffect.EffectTime.Equals(te.EffectTime) && overTimeEffect.TimeBetweenApplyEffect.Equals(te.TimeBetweenApplyEffect))
                    {
                        te.Reset();
                        error = overTimeEffect.name + " it's going to be reseted because it is the same effect";
                    }
                    else if (overTimeEffect.Value > te.Value || overTimeEffect.EffectTime > te.EffectTime || overTimeEffect.TimeBetweenApplyEffect < te.TimeBetweenApplyEffect)
                    {
                        SwitchTimeEffect(te, overTimeEffect);
                        hasBeenAdded = true;

                        error = overTimeEffect.name + " has been added and the previous effect has been removed.";
                    }
                }

                
               
            }

            Debug.Log(error);
            return hasBeenAdded;
        }

        private void SwitchTimeEffect<T, U>(T actualEffect, U newEffect) where T : TimeEffectDefinition where U : TimeEffectDefinition
        {
            actualEffect.Cancel();
            model.TimeEffectDefinitions.Remove(actualEffect);
            model.TimeEffectDefinitions.Add(newEffect);
        }

        public void ChangeRealHealth(StatModificator statModificator)
        {
            model.PerformRealHealthChange(statModificator);
        }
        public void ChangePercentualHealth(StatModificator statModificator)
        {
            model.PerformPercentualHealthChange(statModificator);
        }

        /*public int GetPermanentStat(StatsEnum stat)
        {
            int value = 0;

            switch (stat)
            {
                case StatsEnum.MAX_HEALTH:
                    value = model.MaxHealthWithPermanent;
                    break;
                case StatsEnum.SPEED:
                    value = model.SpeedWithPermanent;
                    break;
            }

                    return value;
        }*/

        public int GetStat(StatsEnum stat)
        {
            return model.GetStat(stat);
        }
    }
}
