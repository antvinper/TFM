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

        float finalEffectLifeTime = 0.0f;

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

            finalEffectLifeTime = GetFinalEffectLifeTime(target.GetStatValue(StatNames.POISE, StatParts.ACTUAL_VALUE));

            OverTimeEffect previousOverTimeEffect = target.GetOverTimeEffectByType(effectType);
            if(previousOverTimeEffect == null || previousOverTimeEffect.isInfinite)
            {
                AddEffect();
            } 
            else
            {
                int effectCase = CheckCase(previousOverTimeEffect);

                switch (effectCase)
                {
                    case 0:
                        Debug.Log("#OverTimeEffect ResetEffect");
                        RestartEffect();
                        break;
                    case 1:
                        Debug.Log("#OverTimeEffect no se aplica");
                        break;
                    case 2:
                    case 3:
                        Debug.Log("#OverTimeEffect reemplaza al anterior");
                        previousOverTimeEffect.CancelEffect();
                        AddEffect();
                        break;
                }
            }
        }

        private void CancelEffect()
        {
            hasBeenCanceled = true;
        }

        private int CheckCase(OverTimeEffect previousOverTimeEffect)
        {
            int effectCase = 0;
            bool hasSomethingInferior = false;
            bool hasSomethingSuperior = false;

            /**
            * Parámetros a checkear:
            * - FinalEffectLifeTime menor
            * - TimeBetweenApplyEffect mayor (es inferior que se aplique cada 2sg que cada 0.5)
            * - Value menor
            * 
            * CASOS
            * Caso 1: Si el nuevo tiene algún parámetro inferior, no se aplica
            * Caso 2: Si el nuevo tiene algún parámetro superior, reemplaza
            * Caso 3: Si el nuevo tiene algún parámetro superior y alguno inferior, reemplaza
            */
            if (!this.Equals(previousOverTimeEffect))
            {
                if (isValueInPercentage)
                {
                    CalculateRealPercentage();
                }
                float previousEffectLifeTime = previousOverTimeEffect.finalEffectLifeTime;
                float previousTimeBetweenApplyEffect = previousOverTimeEffect.timeBetweenApplyEffect;
                float previousValue = previousOverTimeEffect.Value;
                if(this.Value < previousValue || this.finalEffectLifeTime < previousEffectLifeTime || this.timeBetweenApplyEffect > previousTimeBetweenApplyEffect)
                {
                    Debug.Log("#OverTimeEffect tiene algún parámetro inferior");
                    effectCase = 1;
                    hasSomethingInferior = true;
                }
                if(this.Value > previousValue || this.finalEffectLifeTime > previousEffectLifeTime || this.timeBetweenApplyEffect < previousTimeBetweenApplyEffect)
                {
                    Debug.Log("#OverTimeEffect tiene algún parámetro superior");
                    effectCase = 2;
                    hasSomethingSuperior = true;
                }
                if(hasSomethingSuperior && hasSomethingInferior)
                {
                    Debug.Log("#OverTimeEffect tiene algún parámetro superior y alguno inferior");
                    effectCase = 3;
                }
            }

            return effectCase;
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
                    RemoveEffect();
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
                    Debug.Log("#TIMER effect hasBeenCanceled");
                    RemoveEffect();
                    break;
                }
                if (hasBeenStopped)
                {
                    Debug.Log("#TIMER effect hasBeenStopped -> DOING NOTHING");
                }
                else if (actualTimeEffectApplied > finalEffectLifeTime)
                {
                    Debug.Log("#TIMER Effect is over -> Removing effect");
                    RemoveEffect();
                }
                else if (actualTimeBetweenAplpyEffect > timeBetweenApplyEffect)
                {
                    Stat stat = target.GetStatFromName(StatAffected);
                    //Debug.Log(stat.StatName + "." + StatPart + " value before apply the effect " + name + ": " + target.GetStatValue(stat.StatName, StatPart));

                    actualTimeBetweenAplpyEffect = 0.0f;
                    timesApplied += 1;
                    //Debug.Log("#TIMER Effect applied correctly");
                    //target.CalculateStat(StatAffected, StatPart);
                    ApplyEffect();

                    //Debug.Log(stat.StatName + "." + StatPart + " value after apply the effect " + name + ": " + target.GetStatValue(stat.StatName, StatPart));
                }
            } while (finalEffectLifeTime > actualTimeEffectApplied);
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

        private void AddEffect()
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
        }

        public override async Task RemoveEffect(CompanyCharacterController target)
        {
            if (!target.TryRemoveEffect(this))
            {
                Debug.LogError("Effect couldn't be removed");
            }
        }
        public override async Task RemoveEffect()
        {
            if (!target.TryRemoveEffect(this))
            {
                Debug.LogError("Effect couldn't be removed");
            }
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

