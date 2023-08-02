using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : Singleton<ShopManager>
{
    [SerializeField] private Shop shop;
    public Shop Shop
    {
        get => shop;
    }
    public void CreateShop(PlayerController playerController)
    {
        shop.CreateShop(playerController);
    }

    public void ApplyPurchase(int price)
    {
        GameManager.Instance.GetPlayerController().AddRupees(-price);
    }
}
