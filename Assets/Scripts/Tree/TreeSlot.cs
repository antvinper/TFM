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

    [JsonProperty]
    public InstantEffectPermanent Effect
    {
        get => effect;
    }

    [JsonProperty]
    public int ActualActives
    {
        get => actualActives;
    }
    [JsonProperty]
    public int MaxActives
    {
        get => maxActives;
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
