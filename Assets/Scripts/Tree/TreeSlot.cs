using CompanyStats;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TreeSlot
{
    private InstantEffectPermanent effect;
    private int actualActives;
    private int maxActives;
    private int price;
    private int percentualCostPerIncrement;

    public InstantEffectPermanent Effect
    {
        get => effect;
    }

    public int ActualActives
    {
        get => actualActives;
        set => actualActives = value;
    }
    public int MaxActives
    {
        get => maxActives;
    }
    public int Price
    {
        get => price;
    }
    public int PercentualCostPerIncrement
    {
        get => percentualCostPerIncrement;
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
