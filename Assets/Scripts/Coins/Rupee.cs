using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rupee : Coin
{
    public Rupee(int amountValue): base(amountValue)
    {
        this.coinType = CoinTypesEnum.RUPEE;
    }
}
