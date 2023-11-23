using CompanyStats;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


    public abstract class CompanyCharacterController : MonoBehaviour
    {

        [SerializeField] protected CharacterStatsDefinition characterStatsDefinition;
        protected CharacterMutableModel model;
        public CharacterMutableModel Model
        {
            get => model;
            set => model = value;
        }

        public bool TryAddEffect(EffectDefinition effectDefinition, CompanyCharacterController effectOwner, bool isFromTree = false, int index = -1)
        {
            return this.model.TryAddEffect(effectDefinition, isFromTree, index);
        }

        public bool TryRemoveEffect(EffectDefinition effectDefinition)
        {
            return this.model.TryRemoveEffect(effectDefinition);
        }

        public virtual void ApplyDamage(Strike strike)
        {
            Debug.Log("Health before damage = " + model.GetStatValue(StatNames.HEALTH, StatParts.ACTUAL_VALUE));
            bool isAlive = model.ApplyDamage(strike);
            Debug.Log("Applied an attack of: " + strike.FinalValue + " points");
            Debug.Log("Health after damage = " + model.GetStatValue(StatNames.HEALTH, StatParts.ACTUAL_VALUE));

            if (!isAlive)
            {
                Debug.Log("TODO -> Behaviour when dies. Maybe should override method if enemy or player");
            }
        }

        public Stat GetStatFromName(StatNames statName)
        {
            return model.GetStatFromName(statName);
        }

        public int GetStatValue(StatNames statName, StatParts statPart)
        {
            return model.GetStatValue(statName, statPart);
        }

        public DuringTimeEffect GetDuringTimeEffectByType(EffectTypesEnum effectType)
        {
            return model.GetDuringTimeEffectByType(effectType);
        }
        public OverTimeEffect GetOverTimeEffectByType(EffectTypesEnum effectType)
        {
            return model.GetOverTimeEffectByType(effectType);
        }

        public void ChangeActualStatInstantly(EffectDefinition effectDefinition)
        {
            model.ChangeActualStat(effectDefinition);
        }

        public void ChangeActualMaxStatInstantly(EffectDefinition effectDefinition)
        {
            model.ChangeActualMaxStat(effectDefinition);
        }

        /*public virtual CharacterMutableModel Model
        {
            get => model;
            set => model = value;
        }*/
        //public virtual CharacterMutableModel Model { get; set; }
        /*public abstract void ProcessDamage(StatModificator statModificator);

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

        public int GetStat(StatsEnum stat)
        {
            int statValue = 0;
            statValue = model.GetStatValue(stat);
            return model.GetStatValue(stat);
        }

        public int GetActualMaxStat(StatsEnum stat)
        {
            return model.GetActualMaxStatValue(stat);
        }*/
    }


