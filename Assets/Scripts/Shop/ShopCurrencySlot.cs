using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCurrencySlot : ShopSlot
{
    private int soulFragments;
    public void Setup(CurrencyItem currencyItem, int index, Shop shop)
    {
        this.index = index;
        base.Setup(currencyItem, shop);
        soulFragments = currencyItem.SoulFragments;
    }
    public void Purchase()
    {
        /*
         * TODO
         * Get the SoulFragments
         */
        shop.ApplySoulFragmentPurchase(price, soulFragments, this.index);
        base.Purchase();
        //shop.ApplySoulFragmentPurchase(price)
    }

}

