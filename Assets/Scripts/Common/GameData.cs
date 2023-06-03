using Newtonsoft.Json;
using System;
using UnityEngine;

public class GameData
{
    private int slotIndex = 0;
    private int characterLevel = 1;
    private TimeSpan totalPlayTime;
    private int coins = 0;
    private float positionX;
    private float positionY;
    private float positionZ;
    //private Quaternion rotation;


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
    public int Coins
    {
        get => coins;
        set => coins = value;
    }
    public float PositionX
    {
        get => positionX;
        set => positionX = value;
    }
    public float PositionY
    {
        get => positionY;
        set => positionY = value;
    }
    public float PositionZ
    {
        get => positionZ;
        set => positionZ = value;
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
