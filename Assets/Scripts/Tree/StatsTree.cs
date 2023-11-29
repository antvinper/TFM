using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

//[System.Serializable]
public class StatsTree
{
    //[JsonIgnore] 
    private List<TreeSlot> slots;
    //[JsonProperty]
    public List<TreeSlot> Slots
    {
        get => slots;
    }

    public StatsTree(StatsTree statsTree) 
    {
        this.slots = statsTree.Slots;
    }

    public StatsTree(List<TreeSlot> slots)
    {
        this.slots = slots;
    }

    public StatsTree() 
    {
        Debug.Log("Empty Constructor");
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
