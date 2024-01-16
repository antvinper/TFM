using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCurrencySlot : ShopSlot
{
    private int soulFragments;
    public void Setup(CurrencyItem currencyItem, int index, ShopController shopController)
    {
        this.index = index;
        base.Setup(currencyItem, shopController);
        soulFragments = currencyItem.SoulFragments;
    }
    public void Purchase()
    {
        shopController.ApplySoulFragmentPurchase(price, soulFragments, this.index);
        base.Purchase();
    }

}

