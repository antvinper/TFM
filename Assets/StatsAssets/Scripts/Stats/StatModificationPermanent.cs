using CompanyStats;
using Newtonsoft.Json;
using System;
using UnityEngine;

namespace CompanyStats
{
    [System.Serializable]
    public class StatModificationPermanent
    {
        [JsonIgnore] private Guid id;
        [JsonIgnore] private bool isStatIncremented;
        [JsonIgnore] private StatNames statAffected;
        [JsonIgnore] private StatParts statPart;
        [JsonIgnore] private bool isValueInPercentage;
        [JsonIgnore] private StatNames statWhatToSee;
        [JsonIgnore] private bool isTheOwnerStat;
        [JsonIgnore] private StatParts statWhatToSeeStatPart;
        [JsonIgnore] private int valueInPercentage;
        [JsonIgnore] private int value;

        public StatModificationPermanent(InstantEffectPermanentDefinition permanentEffect)
        {
            isStatIncremented = permanentEffect.IsStatIncremented;
            statAffected = permanentEffect.StatAffected;
            statPart = permanentEffect.StatPart;
            isValueInPercentage = permanentEffect.IsValueInPercentage;
            statWhatToSee = permanentEffect.StatWhatToSee;
            isTheOwnerStat = permanentEffect.IsTheOwnerStat;
            statWhatToSeeStatPart = permanentEffect.StatWhatToSeeStatPart;
            valueInPercentage = permanentEffect.ValueInPercentage;
            value = permanentEffect.Value;
        }

        [JsonProperty]
        public Guid Id
        {
            get => id;
        }
        [JsonProperty]
        public bool IsStatIncremented
        {
            get => isStatIncremented;
        }
        [JsonProperty]
        public bool IsValueInPercentage
        {
            get => isValueInPercentage;
        }
        [JsonProperty]
        public bool IsTheOwnerStat
        {
            get => isTheOwnerStat;
        }
        [JsonProperty]
        public StatNames StatAffected
        {
            get => statAffected;
        }
        [JsonProperty]
        public StatNames StatWhatToSee
        {
            get => statWhatToSee;
        }
        [JsonProperty]
        public StatParts StatPart
        {
            get => statPart;
        }
        [JsonProperty]
        public StatParts StatWhatToSeeStatPart
        {
            get => statWhatToSeeStatPart;
        }
        [JsonProperty]
        public int ValueInPercentage
        {
            get => valueInPercentage;
        }
        [JsonProperty]
        public int Value
        {
            get => value;
        }

        public int GetRealValue()
        {
            int realValue = value;

            if (!isStatIncremented)
            {
                realValue = -value;
            }

            return realValue;
        }

        public void CalculateRealPercentage(int statWhatToSeeValue)
        {
            float p = (float) Math.Round((valueInPercentage * 0.01f), 2);
            value = (int)Math.Round(p * statWhatToSeeValue);
        }
    }
}

