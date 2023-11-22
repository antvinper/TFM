using System.Threading.Tasks;
using UnityEngine;

namespace CompanyStats
{
    /**
     * Son aquellos efectos que se aplican siempre debido a los siguientes supuestos:
     * - Se obtiene una mejora que no puede ser eliminada. Por ejemplo, al subir de nivel.
     * - Alguno más?
     */
    public class InstantEffectPermanent : PermanentEffectDefnition
    {
        public override async Task ProcessEffect(CompanyCharacterController target)
        {
            Effect effect = new InstantEffect(this, target, owner);
            effect.ProcessEffect();
        }

        public async Task RemoveEffect(CompanyCharacterController target)
        {
            if (!target.TryRemoveEffect(this))
            {
                Debug.LogError("Something went wrong, and effect couldn't be removed");
            }
        }
    }
}

