using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class StatsTree
{
    private List<TreeSlot> slots;
    public List<TreeSlot> Slots
    {
        get => slots;
    }

    public StatsTree(StatsTreeDefinition statsTreeDefinition)
    {
        slots = new List<TreeSlot>();
        foreach(TreeSlotDefinition treeSlotDefinition in statsTreeDefinition.Slots)
        {
            TreeSlot treeSlot = new TreeSlot(treeSlotDefinition);
            slots.Add(treeSlot);
        }
    }

    public async Task ProcessSlotActivation(TreeSlot slot)
    {
        int index = slots.FindIndex(s => s.Equals(slot));
        slots[index].ProcessSlotActivation();
    }
}
