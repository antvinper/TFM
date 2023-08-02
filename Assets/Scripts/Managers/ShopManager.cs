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

    public void ApplySoulFragmentPurchase(int price, int soulFragmentsToAdd)
    {
        GameManager.Instance.GetPlayerController().AddSoulFragments(soulFragmentsToAdd);
        PayRupees(price);
    }

    public void ApplyComboPurchase(int price)
    {
        PayRupees(price);
    }

    public void ApplyEffectPurchase(int price)
    {
        PayRupees(price);
    }

    private void PayRupees(int price)
    {
        GameManager.Instance.GetPlayerController().AddRupees(-price);
    }
}
