using CompanyStats;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerMutableModel : CharacterMutableModel
{
    private StatsTree tree;
    [JsonProperty]
    public StatsTree Tree
    {
        get => tree;
        set => tree = value;
    }
    [JsonIgnore] private List<StatModificationPermanent> statsModificationPermanentFromTree;

    public PlayerMutableModel(StatsTree tree)
    {
        this.tree = tree;
    }

    public void ProcessSlotTreeActivation(int index)
    {
        TreeSlotDefinition slot = tree.Slots[index];
        tree.ProcessSlotActivation(slot);
        FillStatsModificationPermanentFromTree();
        CalculateStats();
    }
    public void ProcessSlotTreeDeActivation(int index)
    {
        TreeSlotDefinition slot = tree.Slots[index];
        tree.ProcessSlotDeActivation(slot);
        FillStatsModificationPermanentFromTree();
        CalculateStats();
    }

    public override void Setup(CharacterStatsDefinition characterStatsDefinition)
    {
        statsModificationPermanentFromTree = new List<StatModificationPermanent>();

        FillStatsModificationPermanentFromTree();

        base.Setup(characterStatsDefinition);
    }

    private void FillStatsModificationPermanentFromTree()
    {
        statsModificationPermanentFromTree.Clear();
        for (int i = 0; i < tree.Slots.Count; ++i)
        {
            if (tree.Slots[i].IsActive)
            {
                foreach (InstantEffectPermanent effect in tree.Slots[i].Effects)
                {
                    StatModificationPermanent statModificationPermanent = new StatModificationPermanent(effect as InstantEffectPermanentDefinition);
                    statsModificationPermanentFromTree.Add(statModificationPermanent);
                }
            }
        }
    }

    public override void CalculateStat(StatNames statName, StatParts statPart)
    {
        GetStatFromName(statName).ResetStat();

        /*
         * 1º se calculan los cambios permanentes no porcentuales
         */
        CalculateNotPercentualPermanents(statName);
        CalculateNotPercentualPermanentsFromTree(statName);
        /*
         * 2º Se calculan los cambios temporales no porcentuales
         */
        CalculateNotPercentualTemporally(statName);
        /*
         * 3º se calculan los cambios permanentes porcentuales
         */
        CalculatePercentualPermanents(statName);
        CalculatePercentualPermanentsFromTree(statName);
        /*
         * 4º Se calculan los cambios temporales porcentuales
         */
        CalculatePercentualTemporally(statName);
        /*
         * 5º Se calculan los Duringtimeffect cambios temporales
         */
        CalculateDuringTimeEffects();
    }

    private void CalculateNotPercentualPermanentsFromTree(StatNames statName)
    {
        if (statsModificationPermanentFromTree != null)
        {
            List<StatModificationPermanent> statModificationsActives = statsModificationPermanentFromTree.Where(e => e.StatAffected.Equals(statName) && !e.IsValueInPercentage).ToList();
            foreach(StatModificationPermanent statActive in statModificationsActives)
            {
                ChangeStat(statActive);
            }
        }
    }

    private void CalculatePercentualPermanentsFromTree(StatNames statName)
    {
        if (statsModificationPermanentFromTree != null)
        {
            List<StatModificationPermanent> statModificationsActives = statsModificationPermanentFromTree.Where(e => e.StatAffected.Equals(statName) && e.IsValueInPercentage).ToList();
            foreach (StatModificationPermanent statActive in statModificationsActives)
            {
                statActive.GetRealValue();
                ChangeStat(statActive);
            }
        }
    }
}
