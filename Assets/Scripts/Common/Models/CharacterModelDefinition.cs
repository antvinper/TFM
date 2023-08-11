using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Character Model Definition", menuName = "Characters/CharacterModelDefinition")]
public class CharacterModelDefinition : ScriptableObject
{
    [SerializeField] private StatsDefinition statsDefinitions;
    [SerializeField] private StatsTree tree;

    public StatsDefinition StatsDefinitions
    {
        get { return statsDefinitions; }
    }
    public StatsTree Tree
    {
        get { return tree; }
        set { tree = value; }
    }
}
