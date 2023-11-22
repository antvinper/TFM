namespace CompanyStats
{
    [System.Serializable]
    public class StatDefinition
    {
        public StatNames name;
        public int value;
        public int actualMaxValue;
        public int minValue;
        public int maxValue;
        public bool isVolatil;
    }
}

