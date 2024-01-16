using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopComboSlot : ShopSlot
{
    BasicComboDefinition combo;
    public void Setup(ComboItem comboItem, int i, ShopController shopController)
    {
        this.combo = comboItem.Combo;
        this.index = i;
        base.Setup(comboItem, shopController);
    }

    public void Purchase()
    {
        /*
         * TODO
         * Get the combo
         */
        shopController.ApplyComboPurchase(price, combo, index);
        base.Purchase();
    }
}
