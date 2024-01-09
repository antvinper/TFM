using System.Threading.Tasks;
using UnityEngine;

namespace CompanyStats
{
    public abstract class TimeEffectDefinition : TemporallyEffectDefinition
    {
        [Range(0f, float.MaxValue)]
        [SerializeField] protected float effectLifeTime;

        //[HideInInspector] public float EffectLifeTime { get => effectLifeTime; }

        public abstract Task RestartEffect();

        /*public override bool Equals(TimeEffectDefinition other)
        {
            bool isEquals = false;


        }*/

        protected float GetFinalEffectLifeTime(int poise)
        {
            float finalEffectLifeTime = effectLifeTime;
            if (poise > 0)
            {
                float percent = poise * 0.01f;
                finalEffectLifeTime *= percent;
            }
            return finalEffectLifeTime;
        }
    }
}

