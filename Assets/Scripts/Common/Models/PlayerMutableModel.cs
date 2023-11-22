using CompanyStats;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMutableModel : CharacterMutableModel
{
    private StatsTree tree;
    [JsonProperty]
    public StatsTree Tree
    {
        get => tree;
        set => tree = value;
    }

    public PlayerMutableModel(StatsTree tree)
    {
        this.tree = tree;
    }

    public override void Setup(CharacterStatsDefinition characterStatsDefinition)
    {
        statsModificationPermanentFromTree = new List<StatModificationPermanent>();
        foreach(TreeSlotDefinition t in tree.Slots)
        {
            foreach(InstantEffectPermanent effect in t.Effects)
            {
                StatModificationPermanent statModificationPermanent = new StatModificationPermanent(effect as InstantEffectPermanentDefinition);
                statsModificationPermanentFromTree.Add(statModificationPermanent);
            }
            
        }
        base.Setup(characterStatsDefinition);
    }
}
