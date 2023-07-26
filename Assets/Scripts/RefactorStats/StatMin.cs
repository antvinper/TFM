using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatMin
{
    private StatsEnum name;
    private int value;

    public StatMin(StatsEnum name, int value = 0)
    {
        this.name = name;
        this.value = value;
    }

    public StatsEnum Name
    {
        get => name;
    }

    public int Value
    {
        get => value;
    }

    public void AddValue(int value)
    {
        this.value += value;
    }
}
