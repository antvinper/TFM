using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsModifyTime : StatsModify
{
    public override void ApplyStat(StatModificator statModificator)
    {
        //Si es slow down comprobar si hay hurryUp y viceversa. En caso de haberlo, se cancela el otro

        //Lo mismo comprobar con el resto, si hay bajada de algo y viene subida, si hay subida y viene bajada...
        switch (statModificator.BuffDebuffType)
        {
            case EffectTypes.SLOW_DOWN:
                //Comprobar si hay HURRY_UP, si lo hay quitarlo
                break;
            case EffectTypes.POISON:
                int index = stats.FindIndex(s => s.BuffDebuffType.Equals(EffectTypes.REVITALIA));
                if (index != null)
                {

                    //Quitar revitalia y poner poison
                    stats.RemoveAt(index);
                }
                stats.Add(statModificator);
                break;
                //TODO
        }
    }
}
