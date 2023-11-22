using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace CompanyStats
{
    public abstract class TemporallyEffectDefinition : EffectDefinition
    {
        public abstract Task ProcessEffect(CompanyCharacterController owner, CompanyCharacterController target);
        public abstract Task RemoveEffect(CompanyCharacterController target);
        public abstract Task RemoveEffect();
    }
}

