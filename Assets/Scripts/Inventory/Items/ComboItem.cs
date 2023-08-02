using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboItem : Item
{
    [SerializeField] private BasicComboDefinition combo;

    public void Setup(BasicComboDefinition combo)
    {
        this.combo = combo;
        /*
         * TODO
         * Fijar el precio según el número de combo obtenido.
         * Si por ejemplo, no tenemos ningún combo, cualquiera que salga valdrá 10
         * El siguiente que salga, 40...
         */
        finalPrice = combo.Price;
        sprite = combo.Sprite;
        name = combo.Name;
        description = combo.Description;
    }
}
