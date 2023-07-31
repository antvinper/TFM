using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin
{
    protected int amount;
    protected CoinTypesEnum coinType;

    public int Amount
    {
        get => amount;
    }

    protected Coin(int amountValue)
    {
        this.amount = amountValue;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }

    public void RemoveAmount(int value)
    {
        Mathf.Clamp(amount - value, 0, amount);
    }
}
