using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : Singleton<ShopManager>
{
    [SerializeField] private Shop shop;
    public void CreateShop(WeaponController weaponController)
    {
        shop.CreateShop(weaponController);
    }
}
