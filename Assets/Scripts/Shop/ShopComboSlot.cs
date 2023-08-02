using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopComboSlot : ShopSlot
{
    public void Setup(ComboItem comboItem, int i)
    {
        this.index = i;
        base.Setup(comboItem);
    }
}
