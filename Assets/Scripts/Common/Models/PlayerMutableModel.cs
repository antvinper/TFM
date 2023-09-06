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

    public PlayerMutableModel(StatsTree tree)
    {
        this.tree = tree;
    }
}
