using Newtonsoft.Json;

[System.Serializable]
public struct TreeStruct
{
    [JsonProperty]
    public int arrayIndex;
    [JsonProperty]
    public int actualActives;
}
