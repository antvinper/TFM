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
        [JsonIgnore] private string name;
        [JsonIgnore] private bool isStatIncremented;
        [JsonIgnore] private StatNames statAffected;
        [JsonIgnore] private StatParts statPart;
        [JsonIgnore] private bool isValueInPercentage;
        [JsonIgnore] private StatNames statWhatToSee;
        [JsonIgnore] private bool isTheOwnerStat;
        [JsonIgnore] private StatParts statWhatToSeeStatPart;
        [JsonIgnore] private int valueInPercentage;
        [JsonIgnore] private int value;

        public StatModificationPermanent()
        {

        }

        public StatModificationPermanent(InstantEffectPermanentDefinition permanentEffect)
        {
            name = permanentEffect.EffectName;
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
            set => id = value;
        }
        [JsonProperty]
        public bool IsStatIncremented
        {
            get => isStatIncremented;
            set => isStatIncremented = value;
        }
        [JsonProperty]
        public bool IsValueInPercentage
        {
            get => isValueInPercentage;
            set => isValueInPercentage = value;
        }
        [JsonProperty]
        public bool IsTheOwnerStat
        {
            get => isTheOwnerStat;
            set => isTheOwnerStat = value;
        }
        [JsonProperty]
        public StatNames StatAffected
        {
            get => statAffected;
            set => statAffected = value;
        }
        [JsonProperty]
        public StatNames StatWhatToSee
        {
            get => statWhatToSee;
            set => statWhatToSee = value;
        }
        [JsonProperty]
        public StatParts StatPart
        {
            get => statPart;
            set => statPart = value;
        }
        [JsonProperty]
        public StatParts StatWhatToSeeStatPart
        {
            get => statWhatToSeeStatPart;
            set => statWhatToSeeStatPart = value;
        }
        [JsonProperty]
        public int ValueInPercentage
        {
            get => valueInPercentage;
            set => valueInPercentage = value;
        }
        [JsonProperty]
        public int Value
        {
            get => value;
            set => this.value = value;
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

