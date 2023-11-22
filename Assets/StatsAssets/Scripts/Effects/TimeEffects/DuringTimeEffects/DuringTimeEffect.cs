using System.Threading.Tasks;
using UnityEngine;

namespace CompanyStats
{
    public class DuringTimeEffect : TimeEffectDefinition
    {
        bool hasBeenCanceled = false;
        float actualTimeEffectApplied = 0.0f;
        bool resetEffect = false;
        bool hasBeenRemoved = false;
        bool hasBeenStopped = false;

        public override async Task ProcessEffect(CompanyCharacterController owner, CompanyCharacterController target)
        {
            Debug.Log("TODO -> Comprobar con percentage values");
            this.target = target;
            this.owner = owner;
            actualTimeEffectApplied = 0.0f;
            hasBeenStopped = false;
            hasBeenRemoved = false;
            hasBeenCanceled = false;
            resetEffect = false;

            DuringTimeEffect previousDuringTimeEffect = target.GetDuringTimeEffectByType(effectType);

            if(previousDuringTimeEffect == null)
            {
                AddEffectToTarget(false);
            }
            else
            {
                /**
                 * elementos a comprobar:
                 * - IsStatIncremented (Diferente -> Sustituye)
                 */
                previousDuringTimeEffect.RemoveWithoutStop();
                if (IsStatIncremented != previousDuringTimeEffect.IsStatIncremented)
                {
                    previousDuringTimeEffect.RemoveEffect();
                    AddEffectToTarget(false);
                }
                else
                {
                    if (previousDuringTimeEffect.Equals(this))
                    {
                        Debug.Log("They are equals. Restarting effect...");
                        previousDuringTimeEffect.AddEffectToTarget(true);
                        previousDuringTimeEffect.RestartEffect();
                    }
                    else
                    {
                        int effectCase = CheckCase(previousDuringTimeEffect);
                        if(effectCase == 1)
                        {
                            previousDuringTimeEffect.AddEffectToTarget(true);
                            previousDuringTimeEffect.hasBeenStopped = false;
                            Debug.Log("#TIMER finally wont be stopped");
                        }
                        else
                        {
                            previousDuringTimeEffect.CancelEffect();
                            AddEffectToTarget(false);
                        }
                    }
                }
            }
        }

        private int CheckCase(DuringTimeEffect previousDuringTimeEffect)
        {
            int effectCase = 0;
            /**
             * TODO diferenciar entre valores porcentuales o no.
             * Si son valores porcentuales, hay que sacar el valor real del nuevo (sin tener en cuenta el anterior) para comparar
             */
            if (IsValueInPercentage)
            {
                CalculateRealPercentage();
            }

            /**
             * Caso 1: Son iguales, se REINICIA -> OK
             * Caso 2: Si el nuevo tiene algún parámetro inferior, no se aplica NADA -> TODO
             *          - Value menor previous value o 
             *          - Lifetime menor que previous lifetime
             * Caso 3: Si el nuevo tiene algún parámetro superior, reemplaza REEMPLAZAR -> TODO
             *          - Value mayor previous value o 
             *          - Lifetime mayor que previous lifetime
             * Caso 4: Si el nuevo tiene alguno inferior y alguno superior, reemplaza REEMPLAZAR -> TODO
             *          - Value mayor que previous value
             *          - Lifetime menor que previous lifetime
             *          o
             *          - Value menor que previous value
             *          - lifetime mayor que previous lifetime
             * 
             * Reiniciar, nada, reemplazar
             */
            float previousEffectLifeTime = previousDuringTimeEffect.EffectLifeTime;
            float previousValue = previousDuringTimeEffect.Value;
            if(this.Value < previousValue || this.effectLifeTime < previousEffectLifeTime)
            {
                Debug.Log("Caso 1: El efecto no se debe aplicar");
                effectCase = 1;
            }
            if(this.Value > previousValue || this.effectLifeTime > previousEffectLifeTime)
            {
                Debug.Log("Caso 2: El efecto reemplaza");
                effectCase = 2;
            }
            if((this.Value > previousValue && this.effectLifeTime < previousEffectLifeTime) || 
                (this.Value < previousValue && this.effectLifeTime > previousEffectLifeTime))
            {
                Debug.Log("Caso 3: El efecto se reemplaza");
                effectCase = 3;
            }


            return effectCase;
        }

        public override async Task RestartEffect()
        {
            resetEffect = true;
            hasBeenStopped = false;
            /*Stat stat = target.GetStatFromName(StatAffected);
            if (target.TryAddEffect(this, owner))
            {
                Debug.Log(stat.StatName + "." + StatPart + " value after apply the effect " + name + ": " + target.GetStatValue(stat.StatName, StatPart));
                StartTimer();
                Debug.Log("Effect restarted correctly");
            }*/
        }

        private void AddEffectToTarget(bool isReadded)
        {
            if (!isReadded)
            {
                Stat stat = target.GetStatFromName(StatAffected);
                Debug.Log(stat.StatName + "." + StatPart + " value before apply the effect " + name + ": " + target.GetStatValue(stat.StatName, StatPart));
                Debug.Log("TODO Add during time effect");
                if (target.TryAddEffect(this, owner))
                {
                    Debug.Log("Effect applied correctly");
                    Debug.Log(stat.StatName + "." + StatPart + " value after apply the effect " + name + ": " + target.GetStatValue(stat.StatName, StatPart));
                    StartTimer();
                }
            }
            else
            {
                if(target.TryAddEffect(this, owner))
                {
                    Debug.Log("Effect reApplied correctly");
                }
            }
            
        }

        private async Task StartTimer()
        {
            Debug.Log("#TIMER Starting timer");
            do
            {
                //Debug.Log("Time effect applied: " + System.Math.Round(actualTimeEffectApplied, 2));
                //Debug.Log("Time effect applied: ");
                await new WaitForSeconds(Time.deltaTime);
                actualTimeEffectApplied += Time.deltaTime;
                if (resetEffect)
                {
                    Debug.Log("#TIMER Reset effect");
                    actualTimeEffectApplied = 0.0f;
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
            } while (effectLifeTime > actualTimeEffectApplied);
        }

        public void CancelEffect()
        {
            hasBeenCanceled = true;
        }

        public override async Task RemoveEffect(CompanyCharacterController target)
        {
            if (!hasBeenRemoved)
            {
                if (target.TryRemoveEffect(this))
                {
                    hasBeenRemoved = true;
                }
            }
            
        }

        public override async Task RemoveEffect()
        {
            if (!hasBeenRemoved)
            {
                if (target.TryRemoveEffect(this))
                {
                    hasBeenRemoved = true;
                }
            }
        }

        public void RemoveWithoutStop()
        {
            if (target.TryRemoveEffect(this))
            {
                hasBeenStopped = true;
            }
        }
    }
}

