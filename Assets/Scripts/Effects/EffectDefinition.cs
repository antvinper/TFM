using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class EffectDefinition : ScriptableObject, IEffect
{
    protected Characters.CharacterController owner, target;


    #region Always

    [SerializeField] private string effectName;
    [SerializeField] [TextArea] private string description;
    [Tooltip("¿El efecto será aplicado a sí mismo (true) o al target (false)?.")]
    [SerializeField] private bool applyOnSelf;

    public string EffectName => effectName;
    public string Description => description;

    public bool ApplyOnSelf => applyOnSelf;

    [SerializeField]
    [Tooltip("El estado modificado va a ser incrementado o disminuido? Puedo atacar quitando vida o curarme sumando vida.")]
    private bool isStatIncremented;
    public bool IsStatIncremented => isStatIncremented;

    [Tooltip("Estado en el target al cual se le va a aplicar el efecto. Si ataco, el estado afectado en el target será la vida.")]
    [SerializeField] private StatsEnum statAffected;
    public StatsEnum StatAffected => statAffected;


    #endregion

    /**
     * Si el valor es en porcentaje necesito:
     * - El estado qué vamos a mirar pertenece al owner o al target?
     * - El estado del cual sacar ese porcentaje. Por ejemplo queremos un 10% de la velocidad
     * - El valor del porcentaje
     */
    #region IsValueInPercentage
    [Tooltip("¿El estado se va a modificar acorde a un porcentaje?")]
    [SerializeField] protected bool isValueInPercentage;
    public bool IsValueInPercentage => isValueInPercentage;

    [HideInInspector]
    [SerializeField]
    [Tooltip("Este será el estado del cual sacar el porcentaje. Por ejemplo: queremos hacer un daño porcentual en función de la vida del atacante o del target.")]
    protected StatsEnum statWhatToSee;

    [HideInInspector]
    [SerializeField]
    [Tooltip("¿De quién voy a coger el estado por el cual calcular el porcentaje? Siendo owner el dueño de la skil que tiene este efecto. Por ejemplo: quiero curarme un 20% de mi MaxHealth o quiero causar un daño en el enemigo de un 20% de su MaxHealth")]
    protected bool isTheOwnerStat;

    [HideInInspector]
    [SerializeField]
    [Tooltip("¿En qué me voy a basar para coger el estado? ¿En el valor base + las variaciones permanentes = false? ¿En el valor base + todas las variaciones? = true")]
    protected bool useOnlyPermanentStatVariations;

    [HideInInspector]
    [SerializeField]
    [Tooltip("Valor en porcentaje que vamos a coger del estado elegido antes.")]
    [Range(0, 1000)]
    protected int valueInPercentage;
    public int ValueInPercentage => valueInPercentage;
    #endregion

    /**
     * Si el valor no es porcentaje, simplemente asigno un valor manualmente.
     */
    #region IsValueManually
    [HideInInspector]
    [SerializeField]
    [Tooltip("Valor que se desea aplicar sin modificaciones a un stat. Por ejemplo. Quiero curar 200 de Health")]
    [Range(0, 99999)]
    private int value;

    public int Value => value;
    #endregion


    public abstract Task ProcessEffect(Characters.CharacterController owner, Characters.CharacterController target);
    public abstract Task ProcessEffect(Characters.CharacterController target);
    public abstract Task RemoveEffect();

    protected int GetValue()
    {
        return isStatIncremented?Value:-Value;
    }

    protected int GetPercentageValue(Characters.CharacterController owner, Characters.CharacterController target)
    {
        int finalValueInPercentage;
        /*if (useOnlyPermanentStatVariations)
        {
            finalValueInPercentage = GetPercentageValueFromPermanentStat(owner, target);
        }
        else
        {
            finalValueInPercentage = GetPercentageValueFromActualStat(owner, target);
        }*/
        if(this.statWhatToSee.Equals(StatsEnum.MAX_HEALTH))
        {
            finalValueInPercentage = isStatIncremented? valueInPercentage:-valueInPercentage;
        }
        else
        {
            finalValueInPercentage = GetPercentageValueFromActualStat(owner, target);
        }
        
        return finalValueInPercentage;
    }

    /**
     * Si se le ha reducido la rapidez un 10% nos devolverá el valor SIN la reducción
     */
    /*protected int GetPercentageValueFromPermanentStat(Characters.CharacterController owner, Characters.CharacterController target)
    {
        int finalValueInPercentage;

        int statValue = isTheOwnerStat ? owner.GetPermanentStat(statWhatToSee) : target.GetPermanentStat(statWhatToSee);

        finalValueInPercentage = (statValue / 100) * valueInPercentage;

        if (!IsStatIncremented)
        {
            finalValueInPercentage = -finalValueInPercentage;
        }


        return finalValueInPercentage;
    }*/

    /*protected int GetPercentageValueFromActualMaxStat(Characters.CharacterController owner, Characters.CharacterController target)
    {
        float finalValueInPercentage;

        int valueSWTS = isTheOwnerStat ? owner.GetActualMaxStat(statWhatToSee) : target.GetActualMaxStat(statWhatToSee);
    }*/

    /**
     * Si se le ha reducido la rapidez un 10% nos devolverá el valor CON la reducción
     */
    protected int GetPercentageValueFromActualStat(Characters.CharacterController owner, Characters.CharacterController target)
    {
        float finalValueInPercentage;

        int valueSWTS = isTheOwnerStat? owner.GetStat(statWhatToSee) : target.GetStat(statWhatToSee);

        //Hay que comprobar si el stat es el mismo o diferente en el owner stat o igual no y se puede
        //calcular siempre por evitar ifs

        /*if (isTheOwnerStat)
        {
            //finalValueInPercentage = valueInPercentage;
            valueSWTS = owner.GetStat(statWhatToSee);
        }
        else
        {
            valueSWTS = target.GetStat(statWhatToSee);
        }*/

        finalValueInPercentage = (valueSWTS * 0.01f) * valueInPercentage;
        //RECIÉN QUITADA

        /*if (isTheOwnerStat)
        {
            if(statWhatToSee != StatsEnum.ATTACK && statWhatToSee != StatsEnum.MAGIC_ATTACK)
            {
                //finalValueInPercentage = (finalValueInPercentage / owner.GetStat(statAffected)) * 100;
                finalValueInPercentage = (finalValueInPercentage / owner.GetStat(statWhatToSee)) * 100;
            }
            else
            {
                this.isValueInPercentage = false;
            }
        }
        else
        {
            if(statWhatToSee != StatsEnum.ATTACK && statWhatToSee != StatsEnum.MAGIC_ATTACK)
            {
                //finalValueInPercentage = (finalValueInPercentage / target.GetStat(statAffected)) * 100;
                finalValueInPercentage = (finalValueInPercentage / target.GetStat(statWhatToSee)) * 100;
            }
            else
            {
                this.isValueInPercentage = false;
            }
        }*/

        if (isTheOwnerStat)
        {
            //finalValueInPercentage = (finalValueInPercentage / owner.GetStat(statAffected)) * 100;
            finalValueInPercentage = (finalValueInPercentage / owner.GetStat(statWhatToSee)) * 100;
        }
        else
        {
            //finalValueInPercentage = (finalValueInPercentage / target.GetStat(statAffected)) * 100;
            finalValueInPercentage = (finalValueInPercentage / target.GetStat(statWhatToSee)) * 100;
        }


        /*int statValue = isTheOwnerStat ? owner.GetStat(statWhatToSee) : target.GetStat(statWhatToSee);

        finalValueInPercentage = (statValue * 0.01f) * valueInPercentage;*/

        if (!IsStatIncremented)
        {
            finalValueInPercentage = -finalValueInPercentage;
        }


        return (int)finalValueInPercentage;
    }

    protected void ChangeStat(StatModificator statModificator)
    {
        if (ApplyOnSelf)
        {
            Debug.Log(StatAffected + " from " + owner.name + " before apply effect is: " + owner.GetStat(StatAffected));
            owner.ChangeStat(statModificator);
            Debug.Log(StatAffected + " from " + owner.name + " now is: " + owner.GetStat(StatAffected));
        }
        else
        {
            Debug.Log(StatAffected + " from " + target.name + " before apply effect is: " + target.GetStat(StatAffected));
            target.ChangeStat(statModificator);
            Debug.Log(StatAffected + " from " + target.name + " now is: " + target.GetStat(StatAffected));
        }
    }
}
