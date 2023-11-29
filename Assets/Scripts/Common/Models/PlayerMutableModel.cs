using CompanyStats;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class PlayerMutableModel : CharacterMutableModel
{
    private int soulFragments;
    private int rupees;
    [JsonProperty]
    public int Rupees
    {
        get => rupees;
        set => rupees = value;
    }
    [JsonProperty]
    public int SoulFragments
    {
        get => soulFragments;
        set => soulFragments = value;
    }

    private StatsTree tree;
    //[JsonProperty]
    [JsonIgnore]
    public StatsTree Tree
    {
        get => tree;
        set => tree = value;
    }

    private List<TreeStruct> treeStructList;
    [JsonProperty]
    public List<TreeStruct> TreeStructList
    {
        get => treeStructList;
        set => treeStructList = value;
    }

    [JsonIgnore] private List<StatModificationPermanent> statsModificationPermanentFromTree;

    public PlayerMutableModel()
    {

    }

    public PlayerMutableModel(List<TreeStruct> treeStructList)
    {
        this.treeStructList = treeStructList;
    }

    public PlayerMutableModel(StatsTree tree)
    {
        this.tree = tree;
        this.soulFragments = 0;
        this.rupees = 0;
        SetTreeStruct();
    }

    private void SetTreeStruct()
    {
        treeStructList = new List<TreeStruct>();
        for(int i = 0; i < tree.Slots.Count; ++i)
        {
            TreeStruct ts = new TreeStruct();
            ts.actualActives = tree.Slots[i].ActualActives;
            ts.arrayIndex = i;
            treeStructList.Add(ts);
        }
    }

    public void ProcessSlotTreeActivation(int index)
    {
        tree.ProcessSlotActivation(tree.Slots[index]);
        FillStatsModificationPermanentFromTree();
        CalculateStats();
        SetTreeStruct();
    }

    public void ProcessSlotTreeActivation(TreeSlot slot)
    {
        /*TreeSlotDefinition slotToActive = tree.Slots.Where(s => s.Equals(slot)).FirstOrDefault();*/
        tree.ProcessSlotActivation(slot);
        FillStatsModificationPermanentFromTree();
        CalculateStats();
        SetTreeStruct();

    }
    /*public void ProcessSlotTreeDeActivation(int index)
    {
        TreeSlotDefinition slot = tree.Slots[index];
        tree.ProcessSlotDeActivation(slot);
        FillStatsModificationPermanentFromTree();
        CalculateStats();
    }*/

    private void SetupTree()
    {
        foreach(TreeStruct treeStruct in treeStructList)
        {
            tree.Slots[treeStruct.arrayIndex].ActualActives = treeStruct.actualActives;
        }
    }

    public override void Setup(CharacterStatsDefinition characterStatsDefinition)
    {
        SetupTree();
        statsModificationPermanentFromTree = new List<StatModificationPermanent>();

        FillStatsModificationPermanentFromTree();

        base.Setup(characterStatsDefinition);
    }

    private void FillStatsModificationPermanentFromTree()
    {
        statsModificationPermanentFromTree.Clear();
        for (int i = 0; i < tree.Slots.Count; ++i)
        {
            if (tree.Slots[i].ActualActives > 0)
            {
                StatModificationPermanent statModificationPermanent = new StatModificationPermanent(tree.Slots[i].Effect as InstantEffectPermanentDefinition);
                for (int j = 0; j < tree.Slots[i].ActualActives; ++j)
                {
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
