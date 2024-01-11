using Newtonsoft.Json;
using System;
using UnityEngine;

public class GameModel
{
    private int slotIndex;
    private int characterLevel;
    private TimeSpan totalPlayTime;
    //TODO remove
    //private int coins;
    private float positionX;
    private float positionY;
    private float positionZ;
    private float rotationX;
    private float rotationY;
    private float rotationZ;
    private float rotationW;

    private PlayerMutableModel playerModel;
    //private Quaternion rotation;
    private int runLevel;
    private string sceneName;


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
    /*public int Coins
    {
        get => coins;
        set => coins = value;
    }*/
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
    public float RotationX
    {
        get => rotationX;
        set => rotationX = value;
    }
    public float RotationY
    {
        get => rotationY;
        set => rotationY = value;
    }
    public float RotationZ
    {
        get => rotationZ;
        set => rotationZ = value;
    }
    public float RotationW
    {
        get => rotationZ;
        set => rotationZ = value;
    }

    public PlayerMutableModel PlayerModel
    {
        get => playerModel;
        set
        {
            playerModel = value;
        }
    }

    public int RunLevel
    {
        get => runLevel;
        set
        {
            runLevel = value;
        }
    }
    public string SceneName
    {
        get => sceneName;
        set
        {
            sceneName = value;
        }
    }

    public void Setup(int slotIndex)
    {
        /**
         * TODO:
         * All setup needed
         */
        totalPlayTime = TimeSpan.Zero;
        this.slotIndex = slotIndex;
        Debug.Log("New Game Started in slot: " + this.slotIndex);
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
