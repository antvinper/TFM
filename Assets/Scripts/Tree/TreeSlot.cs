using CompanyStats;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

//[System.Serializable]
public class TreeSlot
{
    //[JsonIgnore]
    private InstantEffectPermanent effect;
    //[JsonIgnore] 
    private int actualActives;
    //[JsonIgnore] 
    private int maxActives;
    //[JsonIgnore] 
    private int price;
    //[JsonIgnore]
    private int percentualCostPerIncrement;


    //[JsonProperty]
    public InstantEffectPermanent Effect
    {
        get => effect;
    }

    //[JsonProperty]
    public int ActualActives
    {
        get => actualActives;
        set => actualActives = value;
    }
    //[JsonProperty]
    public int MaxActives
    {
        get => maxActives;
    }
    //[JsonProperty]
    public int Price
    {
        get => price;
    }
    //[JsonProperty]
    public int PercentualCostPerIncrement
    {
        get => percentualCostPerIncrement;
    }



    //public TreeSlot() { }

    public TreeSlot(TreeSlot slot) 
    {
        this.effect = slot.Effect;
        this.actualActives = slot.ActualActives;
        this.maxActives = slot.MaxActives;
        this.price = slot.Price;
        this.percentualCostPerIncrement = slot.PercentualCostPerIncrement;
    }

    public TreeSlot(TreeSlotDefinition treeSlotDefinition, TreeStruct treeStruct)
    {
        effect = treeSlotDefinition.Effect;
        actualActives = treeStruct.actualActives;
        maxActives = treeSlotDefinition.MaxActives;
        price = treeSlotDefinition.Price;
        percentualCostPerIncrement = treeSlotDefinition.PercentualCostPerIncrement;
    }
    public TreeSlot(TreeSlotDefinition treeSlotDefinition)
    {
        effect = treeSlotDefinition.Effect;
        actualActives = treeSlotDefinition.ActualActives;
        maxActives = treeSlotDefinition.MaxActives;
        price = treeSlotDefinition.Price;
        percentualCostPerIncrement = treeSlotDefinition.PercentualCostPerIncrement;
    }

    public int GetActualPrice()
    {
        return (int)(price + (0.1 * percentualCostPerIncrement * actualActives));
    }

    public async Task ProcessSlotActivation()
    {
        if (actualActives < maxActives)
        {
            ++actualActives;
        }
    }
}
