using CompanyStats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Character Model Definition", menuName = "Characters/CharacterModelDefinition")]
public class CharacterModelDefinition : ScriptableObject
{
    [SerializeField] private CharacterStatsDefinition characterStatsDefinitions;
    [SerializeField] private StatsTree tree;

    public CharacterStatsDefinition CharacterStatsDefinitions
    {
        get { return characterStatsDefinitions; }
    }
    public StatsTree Tree
    {
        get { return tree; }
        set { tree = value; }
    }
}
