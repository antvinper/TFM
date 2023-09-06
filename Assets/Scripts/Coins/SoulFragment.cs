using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulFragment : Coin
{
    public SoulFragment(int amountValue) : base(amountValue)
    {
        this.coinType = CoinTypesEnum.SOUL_FRAGMENT;
    }
}
