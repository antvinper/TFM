using UnityEngine;

namespace CompanyStats
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "Character Model Definition", menuName = "Characters/CharacterBaseStatsDefinition")]
    public class CharacterStatsDefinition : ScriptableObject
    {
        public StatsDefinition statsDefinition;
    }
}

