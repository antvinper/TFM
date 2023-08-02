using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopComboSlot : ShopSlot
{
    public void Setup(ComboItem comboItem, int i, Shop shop)
    {
        this.index = i;
        base.Setup(comboItem, shop);
    }

    public void Purchase()
    {
        /*
         * TODO
         * Get the combo
         */
        base.Purchase();
    }
}
