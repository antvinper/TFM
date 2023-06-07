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
                /**
                * TODO
                */
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
                    /**
                    * TODO
                    */
            }

            return value;
        }
    }
}

