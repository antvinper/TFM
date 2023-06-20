using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsModifyInstantTemporallyInPercentage : StatsModify
{
    public override void ApplyStat(StatModificator statModificator)
    {
        stats.Add(statModificator);
        ChangeStat(statModificator.StatToModify, statModificator.Value);
    }
}
