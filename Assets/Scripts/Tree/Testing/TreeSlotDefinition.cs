using CompanyStats;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Tree Slot Definition", menuName = "Tree/Slot Definition")]
public class TreeSlotDefinition : ScriptableObject
{
    //[SerializeField] private List<InstantEffectPermanent> effects = new List<InstantEffectPermanent>();
    [SerializeField] private InstantEffectPermanent effect;

    [Tooltip("Número de veces que se ha mejorado la rama.")]
    [SerializeField] private int actualActives;
    [Tooltip("Máximo número de veces que se puede mejorar la rama.")]
    [SerializeField] private int maxActives;
    [Tooltip("Precio, en fragmentos de alma, que cuesta la primera activación.")]
    [SerializeField] private int price;
    [Tooltip("Incremento de precio en porcentaje por compra.")]
    [Range(0, 1000)]
    [SerializeField] private int percentualCostPerIncrement;

    //[JsonIgnore] 
    /*private int actualCost
    {
        get
        {
            return GetPriceForNext();
        }
    }*/
    public int Price
    {
        get => price;
    }
    //[JsonProperty]
    public int ActualActives
    {
        get => actualActives;
    }

    //[JsonProperty]
    public InstantEffectPermanent Effect
    {
        get => effect;
        set => effect = value;
    }

    //[JsonIgnore]
    /*public int ActualCost
    {
        get
        {
            return GetPriceForNext();
        }
    }*/

    //[JsonIgnore]
    public int MaxActives
    {
        get => maxActives;
    }

    /*private int GetPriceForNext()
    {
        return (int)(price + (0.1 * percentualCostPerIncrement * actualActives));
    }*/


    /*public async Task ProcessSlotActivation()
    {
        //Intentar activar.
        if(actualActives < maxActives)
        {
            ++actualActives;
        }
    }*/

    /*public async Task ProcessSlotDeActivation(CompanyCharacterController target)
    {
        foreach (InstantEffectPermanent effect in effects)
        {
            effect.RemoveEffect(target);
        }

    }*/
}
