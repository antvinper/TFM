using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatModificator
{
    private StatsEnum statToModify;
    private float value;
    private bool isPercentual;
    private bool isAttack = false;
    private bool isPermanent;
    private bool isAlive = true;

    public StatsEnum StatToModify { get; }
    public float Value { get; }
    public bool IsPercentual { get; }
    public bool IsAttack { get; }
    public bool IsPermanent { get; }
    public bool IsAlive { get => isAlive; set => isAlive = value; }

    public StatModificator(StatsEnum statToModify, float value, bool isPercentual, bool isPermanent)
    {
        if(value < 0 && statToModify.Equals(StatsEnum.HEALTH))
        {
            isAttack = true;
        }
        this.statToModify = statToModify;
        this.value = value;
        this.isPercentual = isPercentual;
        this.isPermanent = isPermanent;
    }
}
