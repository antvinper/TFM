using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class Stat
{
    private StatsEnum name;
    private int maxValue;
    private int actualMaxValue; //Nunca puede ser superior a maxValue
    private int minValue;
    private int value;
    //private int actualMaxIncrementInPercentage;

    public Stat(StatsEnum name, int maxValue = 0, int minValue = 0, int value = 0, int actualMaxValue=0)
    {
        this.name = name;
        this.actualMaxValue = actualMaxValue;
        this.maxValue = maxValue;
        this.minValue = minValue;
        this.value = value;
        //this.actualMaxIncrementInPercentage = 0;
    }

    #region Getters
    //Getters
    [JsonProperty]
    public StatsEnum Name
    {
        get => name;
    }
    [JsonProperty]
    public int MaxValue
    {
        get => maxValue;
    }
    [JsonProperty]
    public int ActualMaxValue
    {
        get => actualMaxValue;
    }
    [JsonProperty]
    public int Value
    {
        get => value;
    }
    [JsonProperty]
    public int MinValue
    {
        get => minValue;
    }
    /*public int ActualMaxIncrementInPercentage
    {
        get => actualMaxIncrementInPercentage;
    }*/
    #endregion Getters
    #region Setters
    //ActualMaxValue
    /*public void IncrementActualMaxValueWithConstraints(int value)
    {
        this.actualMaxValue += Mathf.Clamp(actualMaxValue + value, minValue, maxValue);
    }*/
    public void IncrementActualMaxValue(int value)
    {
        this.actualMaxValue += value;
    }

    //MaxValue
    /*public void IncrementMaxValueWithConstraints(int value)
    {
        this.actualMaxValue += Mathf.Clamp(actualMaxValue + value, minValue, maxValue);
    }*/
    public void IncrementMaxValue(int increment)
    {
        this.maxValue += increment;
    }

    //Value
    /*public void IncrementValueWithConstraints(int increment)
    {
        value = Mathf.Clamp(value + increment, minValue, maxValue);
    }*/
    public void IncrementValue(int value)
    {
        this.value += value;
    }

    #endregion Setters


    public override string ToString()
    {
        return "Name: " + name + "; maxValue: " + maxValue + "; actualMaxValue: " + actualMaxValue + "; minValue: " + minValue + "; value: " + value;
    }
}
