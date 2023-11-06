using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Characters
{
    public abstract class CharacterController : MonoBehaviour
    {

        [SerializeField] protected StatsDefinition statsDefinitions;
        protected CharacterMutableModel model;
        public CharacterMutableModel Model
        {
            get => model;
            set => model = value;
        }
        /*public virtual CharacterMutableModel Model
        {
            get => model;
            set => model = value;
        }*/
        //public virtual CharacterMutableModel Model { get; set; }
        public abstract void ProcessDamage(StatModificator statModificator);

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

        private TimeEffectDefinition getActualTimeEffect(EffectTypes type)
        {
            return model.TimeEffectDefinitions.Where(t => t.EffectType.Equals(type)).FirstOrDefault();
        }

        public bool TryAddTemporallyState(DuringTimeEffect duringTimeEffect)
        {
            bool hasBeenAdded = false;
            string error = "";
            /**
             * TENER EN CUENTA EL TIPO NONE. Este no debe influir a la hora de aplicar o no los efectos.
             * Siempre se aplicar� con el tipo NONE
             */
            DuringTimeEffectDefinition te = getActualTimeEffect(duringTimeEffect.EffectType) as DuringTimeEffectDefinition;
            if (te == null)
            {
                error = duringTimeEffect.name + " has been added.";
                model.TimeEffectDefinitions.Add(duringTimeEffect);
                hasBeenAdded = true;
            }
            else
            {
                if (duringTimeEffect.IsValueInPercentage)
                {
                    if (!te.IsValueInPercentage)
                    {
                        error = duringTimeEffect.name + " has been added and the previous effect has been removed.";
                        SwitchTimeEffect(te, duringTimeEffect);
                        hasBeenAdded = true;
                    }
                    else
                    {
                        if (duringTimeEffect.ValueInPercentage.Equals(te.ValueInPercentage) && duringTimeEffect.EffectTime.Equals(te.EffectTime))
                        {
                            error = duringTimeEffect.name + " it's going to be reseted because it is the same effect";
                            te.Reset();
                        }
                        else if (duringTimeEffect.ValueInPercentage > te.ValueInPercentage || duringTimeEffect.EffectTime > te.EffectTime)
                        {
                            error = duringTimeEffect.name + " has been added and the previous effect has been removed.";
                            SwitchTimeEffect(te, duringTimeEffect);
                            hasBeenAdded = true;
                        }
                    }
                }
                else
                {
                    if (duringTimeEffect.Value.Equals(te.Value) && duringTimeEffect.EffectTime.Equals(te.EffectTime))
                    {
                        error = duringTimeEffect.name + " it's going to be reseted because it is the same effect";
                        te.Reset();
                    }
                    else if (duringTimeEffect.Value > te.Value || duringTimeEffect.EffectTime > te.EffectTime)
                    {
                        error = duringTimeEffect.name + " has been added and the previous effect has been removed.";
                        SwitchTimeEffect(te, duringTimeEffect);
                        hasBeenAdded = true;
                    }
                }
                
            }
            return hasBeenAdded;
        }

        public bool TryAddTemporallyState(OverTimeEffect overTimeEffect)
        {
            bool hasBeenAdded = false;
            string error = "";
            OverTimeEffectDefinition te = getActualTimeEffect(overTimeEffect.EffectType) as OverTimeEffectDefinition;

            if (te == null)
            {
                error = overTimeEffect.name + " has been added.";
                model.TimeEffectDefinitions.Add(overTimeEffect);
                hasBeenAdded = true;
            }
            else
            {
                if (overTimeEffect.IsValueInPercentage)
                {
                    if(!te.IsValueInPercentage)
                    {
                        error = overTimeEffect.name + " has been added and the previous effect has been removed.";
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
            actualEffect.RemoveEffect();
            model.TimeEffectDefinitions.Remove(actualEffect);
            model.TimeEffectDefinitions.Add(newEffect);
        }

        public virtual void ChangeRealHealth(StatModificator statModificator)
        {
            statModificator = model.PerformRealHealthChange(statModificator);
            if (!statModificator.IsAlive)
            {
                Debug.Log("TODO -> Character dies");
            }
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
            int statValue = 0;
            statValue = model.GetStatValue(stat);
            return model.GetStatValue(stat);
        }

        public int GetActualMaxStat(StatsEnum stat)
        {
            return model.GetActualMaxStatValue(stat);
        }
    }
}

