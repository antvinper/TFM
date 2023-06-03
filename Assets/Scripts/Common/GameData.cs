using Newtonsoft.Json;
using System;

public class GameData
{
    private int slotIndex = 0;
    private int characterLevel = 1;
    private TimeSpan totalPlayTime;
    public TimeSpan TotalPlaytime { get => GetTotalPlayTime(); set { totalPlayTime = value; } }

    [JsonIgnore] public DateTime startSessionTime;
    [JsonIgnore] public DateTime sessionPlayTime;

    public int CharacterLevel
    {
        get => characterLevel;
        set => characterLevel = value;
    }

    public int SlotIndex { 
        get => slotIndex; 
        set => slotIndex = value; 
    }

    public void Setup()
    {
        /**
         * TODO:
         * All setup needed
         */
        totalPlayTime = TimeSpan.Zero;
    }

    public TimeSpan GetSessionPlayTime()
    {
        return DateTime.Now.Subtract(startSessionTime);
    }

    public TimeSpan GetTotalPlayTime()
    {
        TimeSpan aux = totalPlayTime;

        if (GameManager.Instance.IsGameStarted)
        {
            aux = totalPlayTime + GetSessionPlayTime();
        }

        return aux;
    }
}
