using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace CompanyStats
{
    public abstract class PermanentEffectDefnition : EffectDefinition
{
        public abstract Task ProcessEffect(CompanyCharacterController target);
    }

}

