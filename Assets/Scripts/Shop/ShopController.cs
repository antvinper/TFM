using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    [SerializeField] private ShopView shopView;
    [SerializeField] private ShopModel shopModel;

    private PlayerController playerController;
    private int index = 0;
    private bool canOpenShop = true;

    internal void CreateShop(PlayerController playerController)
    {
        this.playerController = playerController;

        shopModel.Setup();
        shopView.Setup(CloseShop);

        int i = 0;
        i = CreateEffectSlots();
        i = CreateCurrencySlots();
        CreateComboSlot();
    }


    private void OpenShop()
    {
        shopView.ActivateShoppingCanvas();
        playerController.DeActivateControls(false);
        
    }
    private void CloseShop()
    {
        shopView.DeActivateShoppingCanvas();
        playerController.ActivateControls();
    }

    private int CreateEffectSlots()
    {
        foreach(GameObject potion in shopModel.PotionsPrefabs)
        {
            EffectItem effectItem = potion.GetComponent<EffectItem>();
            effectItem.Setup();
            
            shopView.CreateEffectSlots(effectItem, index).Setup(effectItem, index++, this);
        }

        return index;
    }
    private int CreateCurrencySlots()
    {
        foreach(GameObject currency in shopModel.CurrencyPrefabs)
        {
            CurrencyItem currencyItem = currency.GetComponent<CurrencyItem>();
            currencyItem.Setup();
            
            shopView.CreateCurrencySlots(currencyItem, index++).Setup(currencyItem, this);
        }

        return index;
    }
    private void CreateComboSlot()
    {
        int indexCombo = UnityEngine.Random.Range(0, shopModel.CombosInShop.Count);
        BasicComboDefinition combo = shopModel.CombosInShop[indexCombo];
        ComboItem comboItem = new ComboItem();
        comboItem.Setup(combo);

        shopView.CreateComboSlot(comboItem, index).Setup(comboItem, index, this);
    }

    public void ApplySoulFragmentPurchase(int price, int soulFragmentsToAdd, int index)
    {
        GameManager.Instance.GetPlayerController().AddSoulFragments(soulFragmentsToAdd);
        PayRupees(price);

        shopView.ApplyPurchase(index);
    }

    public void ApplyComboPurchase(int price, BasicComboDefinition combo, int index)
    {
        combo.SetActive(true);
        PayRupees(price);

        shopView.ApplyPurchase(index);
    }

    public void ApplyEffectPurchase(int price, EffectItem effectItem, int index)
    {
        effectItem.UseItem(playerController);
        PayRupees(price);

        shopView.ApplyPurchase(index);
    }

    private void PayRupees(int price)
    {
        GameManager.Instance.GetPlayerController().AddRupees(-price);
    }

    internal List<BasicComboDefinition> GetInactiveCombos()
    {
        return GameManager.Instance.GetPlayerController().GetInactiveCombos();
    }

    internal PlayerController GetPlayerController()
    {
        return GameManager.Instance.GetPlayerController();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && canOpenShop)
        {
            OpenShop();
            canOpenShop = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !canOpenShop)
        {
            canOpenShop = true;
        }
    }
}
