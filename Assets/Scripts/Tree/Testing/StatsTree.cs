using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tree Definition", menuName = "Tree/Tree Definition")]
public class StatsTree : ScriptableObject
{
    [SerializeField] private List<TreeSlotDefinition> slots;
    public List<TreeSlotDefinition> Slots
    {
        get { return slots; }
    }
}
