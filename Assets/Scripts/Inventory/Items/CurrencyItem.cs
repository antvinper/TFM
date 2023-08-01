using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyItem : Item
{
    [SerializeField] private int minSoulFragment;
    [SerializeField] private int maxSoulFragment;

    public void Setup()
    {
        int soulFragments = (Random.Range(minSoulFragment, maxSoulFragment + 1));
        finalPrice = soulFragments * price;
        
        nameSufix = " x " + soulFragments;
    }
}
