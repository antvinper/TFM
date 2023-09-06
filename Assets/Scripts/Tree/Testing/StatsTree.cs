using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Tree Definition", menuName = "Tree/Tree Definition")]
public class StatsTree : ScriptableObject
{
    [SerializeField] private List<TreeSlotDefinition> slots;
    [JsonProperty]
    public List<TreeSlotDefinition> Slots
    {
        get => slots;
        set => slots = value;
    }
}
