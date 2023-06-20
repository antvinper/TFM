using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * REFACTOR
 * Usar esta clase como base y crear 2 o 3 diferentes:
 * - Una para los overtimeEffects
 * - Otra para los duringTimeEffects
 * - Otra para los instantEffects
 * - AttackStat?
 * 
 * - Puedo almacenar el SO de cada estado?
 * 
 * Por ejemplo para los instant el timeToBeActive no debería existir, no lo necesita.
 */
public class StatModificator
{
    private EffectTypes buffDebuffType;
    private StatsEnum statToModify;
    private int value;
    private bool isPercentual;
    private bool isAttack = false;
    private bool isPermanent;
    private float timeToBeActive;
    private bool isAlive = true;

    public EffectTypes BuffDebuffType { get => buffDebuffType; }
    public StatsEnum StatToModify { get => statToModify; }
    public int Value { get => value; set => this.value = value; }
    public bool IsPercentual { get => isPercentual; }
    public bool IsAttack { get => isAttack; }
    public bool IsPermanent { get => isPermanent; }
    public float TimeToBeActive { get => timeToBeActive; }
    public bool IsAlive { get => isAlive; set => isAlive = value; }

    public StatModificator(StatsEnum statToModify, int value, bool isPercentual, bool isPermanent, EffectTypes buffDebuffType = EffectTypes.NONE)
    {
        if(value < 0 && statToModify.Equals(StatsEnum.HEALTH))
        {
            isAttack = true;
        }
        this.statToModify = statToModify;
        this.value = value;
        this.isPercentual = isPercentual;
        this.isPermanent = isPermanent;
        this.buffDebuffType = buffDebuffType;
    }


    /**
     * TODO
     * Hacer aquí las lógicas??
     * 
     */
}
