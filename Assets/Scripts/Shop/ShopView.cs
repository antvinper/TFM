using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopView : MonoBehaviour
{
    [SerializeField] EventSystem eventSystem;
    [SerializeField] Button exitButton;
    [SerializeField] Canvas shoppingCanvas;
    [SerializeField] GameObject panelSlots;
    [SerializeField] GameObject slotEffectPrefab;
    [SerializeField] GameObject slotCurrencyPrefab;
    [SerializeField] GameObject slotComboPrefab;

    [SerializeField] List<GameObject> shopSlots;

    internal List<GameObject> ShopSlots;

    internal void Setup(Action callback)
    {
        shopSlots = new List<GameObject>();
        exitButton.onClick.AddListener(callback.Invoke);
    }

    internal void ActivateShoppingCanvas()
    {
        shoppingCanvas.gameObject.SetActive(true);
        SetButtonSelected();
    }

    internal void DeActivateShoppingCanvas()
    {
        shoppingCanvas.gameObject.SetActive(false);
    }

    public void DeActivateSlot(int index)
    {
        shopSlots[index].gameObject.SetActive(false);
    }

    public void DeActivateEffectSlots()
    {
        List<GameObject> effectSlots = shopSlots.Where(t => t.GetComponent<ShopEffectSlot>()).ToList();

        foreach (GameObject go in effectSlots)
        {
            DeActivateSlot(go.GetComponent<ShopEffectSlot>().Index);
        }
    }

    internal ShopEffectSlot CreateEffectSlots(EffectItem effectItem, int index)
    {
        GameObject go = Instantiate(slotEffectPrefab, panelSlots.transform);
        shopSlots.Add(go);

        return shopSlots[index].GetComponent<ShopEffectSlot>();
    }

    internal ShopCurrencySlot CreateCurrencySlots(CurrencyItem currencyItem, int index)
    {
        GameObject go = Instantiate(slotCurrencyPrefab, panelSlots.transform);
        shopSlots.Add(go);

        return shopSlots[index].GetComponent<ShopCurrencySlot>();
    }

    internal ShopComboSlot CreateComboSlot(ComboItem comboItem, int index)
    {
        GameObject go = Instantiate(slotComboPrefab, panelSlots.transform);
        shopSlots.Add(go);

        return shopSlots[index].GetComponent<ShopComboSlot>();
    }

    internal void ApplyPurchase(int index)
    {
        foreach (GameObject go in shopSlots)
        {
            go.GetComponent<ShopSlot>().CheckButtonInteractability();
        }
        SetButtonSelected();
    }

    internal void SetButtonSelected()
    {
        bool hasBeenSelected = false;

        eventSystem.SetSelectedGameObject(exitButton.gameObject);

        for(int i = 0; i < shopSlots.Count; ++i)
        {
            hasBeenSelected = shopSlots[i].GetComponent<ShopSlot>().SetButtonAsFirstSelected(eventSystem);
            if (hasBeenSelected)
            {
                break;
            }
        }
    }
}
