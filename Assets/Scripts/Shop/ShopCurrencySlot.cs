using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCurrencySlot : ShopSlot
{
    public void Setup(CurrencyItem currencyItem, int index, Shop shop)
    {
        this.index = index;
        base.Setup(currencyItem, shop);
    }
    public void Purchase()
    {
        /*
         * TODO
         * Get the SoulFragments
         */
        base.Purchase();
    }

}

