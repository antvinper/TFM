using UnityEngine;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CompanyStats
{
    public abstract class EffectDefinition : ScriptableObject, IEffect
    {
        protected CompanyCharacterController owner, target;
        [SerializeField] private string effectName;

        [SerializeField] protected EffectTypesEnum effectType;

        [SerializeField][TextArea] private string description;

        [Tooltip("�El efecto ser� aplicado a s� mismo (true) o al target (false)?.")]
        [SerializeField] private bool applyOnSelf;

        [Tooltip("El estado modificado va a ser incrementado o disminuido? Puedo atacar quitando vida o curarme sumando vida.")]
        [SerializeField] private bool isStatIncremented;

        [Tooltip("Estado en el target al cual se le va a aplicar el efecto. Si ataco, el estado afectado en el target ser� la vida.")]
        [SerializeField] private StatNames statAffected;

        [Tooltip("Parte del estado que va a ser modificado. Valor actual, mínimo, máximo actual o máximo.")]
        [SerializeField] private StatParts statPart;



        [Tooltip("�El estado se va a modificar acorde a un porcentaje?")]
        [SerializeField] protected bool isValueInPercentage;

        #region IsValueInPercentage

        [HideInInspector]
        [Tooltip("Este ser� el estado del cual sacar el porcentaje. Por ejemplo: queremos hacer un da�o porcentual en funci�n de la vida del atacante o del target.")]
        [SerializeField] protected StatNames statWhatToSee;

        [HideInInspector]
        [Tooltip("�De qui�n voy a coger el estado por el cual calcular el porcentaje? Siendo owner el due�o de la skil que tiene este efecto. Por ejemplo: quiero curarme un 20% de mi MaxHealth o quiero causar un da�o en el enemigo de un 20% de su MaxHealth")]
        [SerializeField] protected bool isTheOwnerStat;
        
        [HideInInspector]
        [Tooltip("Parte del estado del cual mirar. Valor actual, mínimo, máximo actual o máximo.")]
        [SerializeField] private StatParts statWhatToSeeStatPart;

        /*[HideInInspector]
        [Tooltip("�En qu� me voy a basar para coger el estado? �En el valor base + las variaciones permanentes = false? �En el valor base + todas las variaciones? = true")]
        [SerializeField] protected bool useOnlyPermanentStatVariations;*/

        [HideInInspector]
        [Tooltip("Valor en porcentaje que vamos a coger del estado elegido antes.")]
        [Range(0, 1000)]
        [SerializeField] protected int valueInPercentage;

        #endregion

        #region IsValueNatural

        [HideInInspector]
        [Tooltip("Valor que se desea aplicar sin modificaciones a un stat. Por ejemplo. Quiero curar 200 de Health")]
        [Range(0, 99999)]
        [SerializeField] private int value;

        #endregion
        protected bool hasBeenApplied = false;
        [JsonProperty]
        public bool HasBeenApplied { get => hasBeenApplied; set => hasBeenApplied = value; }

        [JsonProperty]
        public string EffectName
        {
            get => effectName;
            set => effectName = value;
        }
        [JsonProperty]
        public EffectTypesEnum EffectType
        {
            get => effectType;
            set => effectType = value;
        }

        [JsonProperty]
        public string Description
        {
            get => description;
            set => description = value;
        }

        [JsonProperty]
        public bool ApplyOnSelf
        {
            get => applyOnSelf;
            set => applyOnSelf = value;
        }

        [JsonProperty]
        public bool IsStatIncremented
        {
            get => isStatIncremented;
            set => isStatIncremented = value;
        }

        [JsonProperty]
        public StatNames StatAffected
        {
            get => statAffected;
            set => statAffected = value;
        }
        [JsonProperty]
        public StatParts StatPart
        {
            get => statPart;
            set => statPart = value;
        }
        [JsonProperty]
        public StatParts StatWhatToSeeStatPart
        {
            get => statWhatToSeeStatPart;
            set => statWhatToSeeStatPart = value;
        }
        [JsonProperty]
        public int Value
        {
            get => value;
            set => this.value = value;
        }
        [JsonProperty]
        public bool IsValueInPercentage
        {
            get => isValueInPercentage;
            set => isValueInPercentage = value;
        }
        [JsonProperty]
        public int ValueInPercentage
        {
            get => valueInPercentage;
            set => valueInPercentage = value;
        }

        [JsonProperty]
        public bool IsTheOwnerStat
        {
            get => isTheOwnerStat;
            set => isTheOwnerStat = value;
        }
        [JsonProperty]
        public StatNames StatWhatToSee
        {
            get => statWhatToSee;
            set => statWhatToSee = value;
        }

        public int GetRealValue()
        {
            int realValue = value;

            if(!isStatIncremented)
            {
                realValue = -value;
            }

            return realValue;
        }

        public void CalculateRealPercentage()
        {
            //Calcular el valor real en función del statWhatToSee.
            
            //Obtengo el valor del stat
            int statWhatToSeeValue = IsTheOwnerStat ?
                owner.GetStatValue(statWhatToSee, statWhatToSeeStatPart) :
                target.GetStatValue(statWhatToSee, statWhatToSeeStatPart);

            //Calculo el valor según el porcentaje
            float p = (float)System.Math.Round((valueInPercentage * 0.01f), 2);
            value = (int)(p * statWhatToSeeValue);

            //Debug.Log(value);
        }

    }
}

