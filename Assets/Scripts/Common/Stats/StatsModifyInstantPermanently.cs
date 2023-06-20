using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsModifyInstantPermanently : StatsModify
{
    public override void ApplyStat(StatModificator statModificator)
    {
        stats.Add(statModificator);
        ChangeStat(statModificator.StatToModify, statModificator.Value);
        if (statModificator.StatToModify.Equals(StatsEnum.MAX_HEALTH))
        {
            ChangeStat(StatsEnum.HEALTH, statModificator.Value);
        }
    }
}
