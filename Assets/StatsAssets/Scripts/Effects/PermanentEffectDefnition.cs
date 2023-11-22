using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace CompanyStats
{
    public abstract class PermanentEffectDefnition : EffectDefinition
    {
        /*private bool isAppliedInRun;
        public bool IsAppliedInRun
        {
            get => isAppliedInRun;
            set => isAppliedInRun = value;
        }*/
        public abstract Task ProcessEffect(CompanyCharacterController target);
    }

}

