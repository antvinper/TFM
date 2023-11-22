using System.Collections.Generic;
using UnityEngine;

namespace CompanyStats
{
    [CreateAssetMenu(fileName = "Stat Definition", menuName = "Stats/Stat Definition")]
    public class StatsDefinition : ScriptableObject
    {
        public List<StatDefinition> stats = new List<StatDefinition>();
    }
}

