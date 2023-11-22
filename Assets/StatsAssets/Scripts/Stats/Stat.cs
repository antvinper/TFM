using Newtonsoft.Json;
using System.Collections.Generic;

namespace CompanyStats
{
    [System.Serializable]
    public class Stat
    {
        private StatNames statName;
        private int value;
        private int actualMaxValue;
        private int maxValue;
        private int minValue;

        private int baseValue;
        private int baseActualMaxValue;
        private int baseMaxValue;
        private int baseMinValue;

        private bool isVolatil;

        [JsonProperty]
        public StatNames StatName { get { return statName; } }
        [JsonProperty]
        public int MaxValue { get { return maxValue; } }
        [JsonProperty]
        public int MinValue { get { return minValue; } }
        [JsonProperty]
        public int Value { get { return value; } }
        [JsonProperty]
        public int ActualMaxValue { get { return actualMaxValue; } }
        [JsonProperty]
        public bool IsVolatil { get { return isVolatil; } }

        public Stat(StatNames statName, int value, int actualMaxValue, int maxValue, int minValue, bool isVolatil)
        {
            this.statName = statName;
            this.value = value;
            this.actualMaxValue = actualMaxValue;
            this.maxValue = maxValue;
            this.minValue = minValue;
            this.isVolatil = isVolatil;

            this.baseValue = value;
            this.baseMinValue = minValue;
            this.baseActualMaxValue = actualMaxValue;
            this.baseMaxValue = maxValue;
        }

        public void HardReset()
        {
            value = baseValue;
            actualMaxValue = baseActualMaxValue;
            maxValue = baseMaxValue;
            minValue = baseMinValue;
        }

        public void ResetStat()
        {
            if (!isVolatil)
            {
                value = baseValue;
            }
            
            actualMaxValue = baseActualMaxValue;
            maxValue = baseMaxValue;
            minValue = baseMinValue;
        }

        public virtual void DecreaseValue(int value)
        {
            int preValue = this.value - value;
            this.value = preValue < 0 ? 0 : preValue;
        }

        public virtual void DecreaseValueInPercentage(int valueInPercentage)
        {
            float percent = valueInPercentage * 0.01f;
            int preValue = (int) (this.value * percent);
            this.value = preValue < 0 ? 0 : preValue;
        }

        public virtual void IncreaseValue(int value)
        {
            int preValue = this.value + value;
            this.value = preValue > actualMaxValue ? actualMaxValue : preValue;
        }

        public virtual void IncreaseValueInPercentage(int valueInPercentage)
        {
            float percent = (valueInPercentage * 0.01f) + 1;
            int preValue = (int)(this.value * percent);
            this.value = preValue > actualMaxValue ? actualMaxValue : preValue;
        }

        public virtual void IncreaseActualMaxValue(int value)
        {
            int preValue = this.actualMaxValue + value;
            this.actualMaxValue = preValue > maxValue ? maxValue : preValue;
        }
        public virtual void IncreaseActualMaxValueInPercentage(int valueInPercentage)
        {
            float percent = (valueInPercentage * 0.01f) + 1;
            int preValue = (int)(this.actualMaxValue * percent);
            this.actualMaxValue = preValue > maxValue ? maxValue : preValue;
        }

        public virtual void DecreaseActualMaxValue(int value)
        {
            int preValue = this.actualMaxValue - value;
            this.actualMaxValue = preValue < minValue ? minValue : preValue;
        }
        public virtual void DecreaseActualMaxValueInPercentage(int valueInPercentage)
        {
            float percent = (valueInPercentage * 0.01f);
            int preValue = (int)(this.actualMaxValue * percent);
            this.actualMaxValue = preValue < minValue ? minValue : preValue;
        }
    }
}

