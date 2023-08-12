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
    }
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

    public PlayerMutableModel(StatsTree tree)
    {
        this.tree = tree;
    }
}
