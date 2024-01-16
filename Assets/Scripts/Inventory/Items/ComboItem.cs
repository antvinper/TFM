using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboItem : Item
{
    [SerializeField] private BasicComboDefinition combo;
    public BasicComboDefinition Combo => combo;

    public void Setup(BasicComboDefinition combo)
    {
        this.combo = combo;
        /*
         * TODO
         * Fijar el precio seg�n el n�mero de combo obtenido.
         * Si por ejemplo, no tenemos ning�n combo, cualquiera que salga valdr� 10
         * El siguiente que salga, 40...
         */
        finalPrice = combo.Price;
        sprite = combo.Sprite;
        name = combo.Name;
        description = combo.Description;
    }
}
