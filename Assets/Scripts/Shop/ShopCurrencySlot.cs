using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCurrencySlot : ShopSlot
{
    public void Setup(CurrencyItem currencyItem, int index)
    {
        this.index = index;
        base.Setup(currencyItem);
    }
}
