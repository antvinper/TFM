using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat
{
    private StatsEnum name;
    private int maxValue;
    private int actualMaxValue;
    private int minValue;
    private int value;

    public Stat(StatsEnum name, int maxValue, int minValue, int value, int actualMaxValue=255)
    {
        this.name = name;
        this.actualMaxValue = actualMaxValue;
        this.maxValue = maxValue;
        this.minValue = minValue;
        this.value = value;
    }

    public void IncrementValue(int increment)
    {
        value = Mathf.Clamp(value + increment, minValue, maxValue);
    }

    public void DecrementValue(int decrement)
    {
        value = Mathf.Clamp(value - decrement, minValue, maxValue);
    }

    public StatsEnum Name
    {
        get => name;
    }

    public int Value
    {
        get => value;
        set => this.value = value;
    }
    public int ActualMaxValue
    {
        get => actualMaxValue;
        set => this.actualMaxValue = value;
    }

    public override string ToString()
    {
        return "Name: " + name + "; maxValue: " + maxValue + "; actualMaxValue: " + actualMaxValue + "; minValue: " + minValue + "; value: " + value;
    }
}
