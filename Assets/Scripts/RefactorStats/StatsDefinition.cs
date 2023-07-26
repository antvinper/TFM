using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stat Definition", menuName = "Stats/Stat Definition")]
public class StatsDefinition : ScriptableObject
{
    public List<StatDefinition> stats = new List<StatDefinition>();
}

[System.Serializable]
public class StatDefinition
{
    public StatsEnum name;
    public int maxValue;
    public int actualMaxValue;
    public int minValue;
    public int value;
}
