using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters
{
    public abstract class CharacterController : MonoBehaviour
    {
        [SerializeField] protected CharacterMutableModel model;
        public abstract void ProcessDamage(float value);

        //public abstract void ProcessEffect(CharacterMutableModel attacker);
        //public abstract void ProcessHealth(float value);
        //public abstract void ProcessEffectOverTime();

        public abstract float GetMyRealDamage();

        public void SetStat(StatsEnum stat, float value)
        {
            switch(stat)
            {
                case StatsEnum.SPEED:
                    model.Speed = (int)value;
                    break; 
                case StatsEnum.HEALTH:
                    model.Health = (int)value;
                    break;
                /**
                * TODO
                */
            }
        }

        /**
         * TODO
         * Refactor para pasa una estructura y tener sólo un ChangeStat
         * Tener en cuenta si es un ataque o una curación cuando se vea afectada la Health. Por ejemplo para mostrar los números en verde o en rojo
         */

        public void ChangeStatPermanent(StatsEnum stat, float value)
        {
            switch (stat)
            {
                case StatsEnum.MAX_HEALTH:
                    model.StatsIncrement.MaxHealth += (int)value;
                    break;
                /**
                 * TODO
                 */
            }
        }

        public void ChangeStatInRun(StatsEnum stat, float value)
        {
            switch(stat)
            {
                case StatsEnum.HEALTH:
                    /**
                     * TODO:
                     * Tener en cuenta en la struct si es un ataque o una curación
                     */
                    if(value < 0)
                    {
                        bool isDead = model.TakeDamage(value);
                    }
                    else
                    {
                        model.Heal(value);
                    }
                    
                    break;
                case StatsEnum.MAX_HEALTH:
                    /**
                     * TODO
                     * hay que modificar 
                     */
                    break;
            }
        }
        public void ChangePercentualStatInRun(StatsEnum stat, float value)
        {
            switch(stat)
            {
                case StatsEnum.HEALTH:
                    if(value < 0)
                    {
                        bool isDead = model.TakePercentualDamage(value);
                    }
                    else
                    {
                        model.Heal(value);
                    }
                    break;
                case StatsEnum.MAX_HEALTH:
                    /**
                     * TODO
                     * hay que modificar 
                     */
                    break;
            }
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

