using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopComboSlot : ShopSlot
{
    BasicComboDefinition combo;
    public void Setup(ComboItem comboItem, int i, Shop shop)
    {
        this.combo = comboItem.Combo;
        this.index = i;
        base.Setup(comboItem, shop);
    }

    public void Purchase()
    {
        /*
         * TODO
         * Get the combo
         */
        shop.ApplyComboPurchase(price, combo, index);
        base.Purchase();
    }
}
