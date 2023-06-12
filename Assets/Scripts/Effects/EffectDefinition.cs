using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class EffectDefinition : ScriptableObject, IEffect
{

    #region Always

    [SerializeField] private string effectName;
    [SerializeField] [TextArea] private string description;

    public string EffectName => effectName;
    public string Description => description;

    

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
    [Range(0, 100)]
    protected int valueInPercentage;
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

    protected float GetValue()
    {
        return isStatIncremented?Value:-Value;
    }

    protected float GetPercentageValue(Characters.CharacterController owner, Characters.CharacterController target)
    {
        float finalValueInPercentage;
        if (useOnlyPermanentStatVariations)
        {
            finalValueInPercentage = GetPercentageValueFromPermanentStat(owner, target);
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
    protected float GetPercentageValueFromPermanentStat(Characters.CharacterController owner, Characters.CharacterController target)
    {
        float finalValueInPercentage;

        float statValue = isTheOwnerStat ? owner.GetPermanentStat(statWhatToSee) : target.GetPermanentStat(statWhatToSee);

        finalValueInPercentage = (statValue / 100) * valueInPercentage;

        if (!IsStatIncremented)
        {
            finalValueInPercentage = -finalValueInPercentage;
        }


        return finalValueInPercentage;
    }

    /**
     * Si se le ha reducido la rapidez un 10% nos devolverá el valor CON la reducción
     */
    protected float GetPercentageValueFromActualStat(Characters.CharacterController owner, Characters.CharacterController target)
    {
        float finalValueInPercentage;

        float statValue = isTheOwnerStat ? owner.GetStat(statWhatToSee) : target.GetStat(statWhatToSee);

        finalValueInPercentage = (statValue / 100) * valueInPercentage;

        if (!IsStatIncremented)
        {
            finalValueInPercentage = -finalValueInPercentage;
        }


        return finalValueInPercentage;
    }
}
