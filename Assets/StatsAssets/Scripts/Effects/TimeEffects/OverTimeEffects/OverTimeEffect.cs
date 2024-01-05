using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace CompanyStats
{
    public class OverTimeEffect : TimeEffectDefinition
    {
        public int timesApplied = 0;
        bool hasBeenCanceled = false;
        float actualTimeEffectApplied = 0.0f;
        float actualTimeBetweenAplpyEffect = 0.0f;
        bool resetEffect = false;
        bool hasBeenRemoved = false;
        bool hasBeenStopped = false;

        [SerializeField]
        bool isInfinite = false;
        public bool IsInfinite
        {
            get => isInfinite;
        }
        /**
         * TODO
         * Esto debe ser elegible.
         * Si es un combate por turnos, esto debe referenciar a los turnos
         * Si es un combate de acción, lo suyo sería que fuese por tiempo
         */
        [SerializeField]
        [Range(0f, float.MaxValue)]
        protected float timeBetweenApplyEffect;

        public float TimeBetweenApplyEffect { get => timeBetweenApplyEffect; }

        public override async Task ProcessEffect(CompanyCharacterController owner, CompanyCharacterController target)
        {
            this.target = target;
            this.owner = owner;
            actualTimeEffectApplied = 0.0f; 
            actualTimeBetweenAplpyEffect = 0.0f;
            hasBeenStopped = false;
            hasBeenRemoved = false;
            hasBeenCanceled = false;
            resetEffect = false;
            timesApplied = 0;

            OverTimeEffect previousOverTimeEffect = target.GetOverTimeEffectByType(effectType);
            if(previousOverTimeEffect == null)
            {
                if (target.TryAddEffect(this, owner))
                {
                    if (isInfinite)
                    {
                        StartInfiniteTimer();
                    }
                    else
                    {
                        StartTimer();
                    }
                }
            } else
            {
                if (this.Equals(previousOverTimeEffect))
                {
                    RestartEffect();
                }
                else
                {
                    Debug.Log("TODO Realizar los checks");
                }
            }
            
        }

        private async Task StartInfiniteTimer()
        {
            do
            {
                await new WaitForSeconds(Time.deltaTime);
                actualTimeEffectApplied += Time.deltaTime;
                actualTimeBetweenAplpyEffect += Time.deltaTime;
                if (hasBeenCanceled)
                {
                    Debug.Log("#TIMER hasBeenCanceled");
                    RemoveEffect(target);
                    break;
                }
                else if (actualTimeBetweenAplpyEffect > timeBetweenApplyEffect)
                {
                    Stat stat = target.GetStatFromName(StatAffected);
                    Debug.Log(stat.StatName + "." + StatPart + " value before apply the effect " + name + ": " + target.GetStatValue(stat.StatName, StatPart));

                    actualTimeBetweenAplpyEffect = 0.0f;
                    timesApplied += 1;
                    //Debug.Log("#TIMER Effect applied correctly");
                    //target.CalculateStat(StatAffected, StatPart);
                    ApplyEffect();

                    Debug.Log(stat.StatName + "." + StatPart + " value after apply the effect " + name + ": " + target.GetStatValue(stat.StatName, StatPart));
                }
            } while (!hasBeenCanceled);
        }

        private async Task StartTimer()
        {
            do
            {
                await new WaitForSeconds(Time.deltaTime);
                actualTimeEffectApplied += Time.deltaTime;
                actualTimeBetweenAplpyEffect += Time.deltaTime;
                if (resetEffect)
                {
                    Debug.Log("#TIMER Reset effect");
                    actualTimeEffectApplied = 0.0f;
                    actualTimeBetweenAplpyEffect = 0.0f;
                    resetEffect = false;
                }
                if (hasBeenCanceled)
                {
                    Debug.Log("#TIMER hasBeenCanceled");
                    RemoveEffect(target);
                    break;
                }
                if (hasBeenStopped)
                {
                    Debug.Log("#TIMER hasBeenStopped");
                }
                else if (actualTimeEffectApplied > effectLifeTime)
                {
                    Debug.Log("#TIMER actual time bigger");
                    RemoveEffect(target);
                }
                else if (actualTimeBetweenAplpyEffect > timeBetweenApplyEffect)
                {
                    Stat stat = target.GetStatFromName(StatAffected);
                    Debug.Log(stat.StatName + "." + StatPart + " value before apply the effect " + name + ": " + target.GetStatValue(stat.StatName, StatPart));

                    actualTimeBetweenAplpyEffect = 0.0f;
                    timesApplied += 1;
                    Debug.Log("#TIMER Effect applied correctly");
                    //target.CalculateStat(StatAffected, StatPart);
                    ApplyEffect();

                    Debug.Log(stat.StatName + "." + StatPart + " value after apply the effect " + name + ": " + target.GetStatValue(stat.StatName, StatPart));
                }
            } while (effectLifeTime > actualTimeEffectApplied);

            Debug.Log("#TIMER Time finished. Here i should remove effect.");
            if (!target.TryRemoveEffect(this))
            {
                Debug.LogError("Effect couldn't be removed");
            }
        }

        private void ApplyEffect()
        {
            /**
             *  TODO
             *  Quizás conviene crear la clase PoisonEffect y RevitaliaEffect
             *  Si se quisiera se crean más y se queda este código más limpio.
             *  Al igual que con AttackEffect y HealEffect.
             */
            if (isValueInPercentage)
            {
                CalculateRealPercentage();
            }
            switch (effectType)
            {
                case EffectTypesEnum.POISON:
                    Strike strike = new Strike(owner, target, this);
                    strike.ProcessStrike();
                    target.ApplyDamage(strike);
                    break;
                case EffectTypesEnum.REVITALIA:
                    target.ChangeActualStatInstantly(this);
                    break;
                //TODO MORE?
            }
        }

        public override async Task RemoveEffect(CompanyCharacterController target)
        {
            throw new System.NotImplementedException();
        }
        public override async Task RemoveEffect()
        {
            throw new System.NotImplementedException();
        }
        public override async Task RestartEffect()
        {
            resetEffect = true;
        }


        /*public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            else
            {
                OverTimeEffect other = obj as OverTimeEffect;

                if (other.ApplyOnSelf != this.ApplyOnSelf)
                {
                    return false;
                }
                else if(other.EffectLifeTime != this.EffectLifeTime)
                {
                    return false;
                }
            }
            
            return base.Equals(obj);
        }*/
    }
}

