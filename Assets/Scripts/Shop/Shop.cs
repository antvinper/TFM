using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//[System.Serializable]
public class Shop: MonoBehaviour
{
    /*[SerializeField] Canvas shoppingCanvas;
    [SerializeField] GameObject panelSlots;
    [SerializeField] GameObject slotEffectPrefab;
    [SerializeField] GameObject slotCurrencyPrefab;
    [SerializeField] GameObject slotComboPrefab;

    [SerializeField] List<GameObject> shopSlots;*/
    //[SerializeField] private List<GameObject> potionsPrefabs;
    //[SerializeField] private List<GameObject> currencyPrefabs;

    /*[SerializeField] private List<BasicComboDefinition> swordCombos;
    [SerializeField] private List<BasicComboDefinition> chackramCombos;*/
    //[SerializeField] private List<BasicComboDefinition> combosInShop;

    //public void CreateShop()
    //{
        //this.playerController = playerController;
        /*this.playerController = playerController;
        WeaponController weaponController = playerController.weaponController;*/

        //combosInShop = ShopController.Instance.GetInactiveCombos();

        /*if(weaponController is SwordController)
        {
            combosInShop = swordCombos;
        } else if(weaponController is ChackramController)
        {
            combosInShop = chackramCombos;
        }*/

        //shopSlots = new List<GameObject>();
        /*int i = 0;
        i = CreateEffectSlots(i);

        
        i = CreateCurrencySlots(i);

        CreateComboSlot(i);

        shoppingCanvas.gameObject.SetActive(true);*/
    //}

    /*private void CreateComboSlot(int i)
    {
        GameObject go = Instantiate(slotComboPrefab, panelSlots.transform);
        shopSlots.Add(go);

        int indexCombo = Random.Range(0, combosInShop.Count);
        BasicComboDefinition bcd = combosInShop[indexCombo];
        ComboItem comboItem = new ComboItem();
        comboItem.Setup(bcd);
        shopSlots[i].GetComponent<ShopComboSlot>().Setup(comboItem, i, this);
    }*/

    /*private int CreateCurrencySlots(int i)
    {
        foreach (GameObject currency in currencyPrefabs)
        {
            GameObject go = Instantiate(slotCurrencyPrefab, panelSlots.transform);
            shopSlots.Add(go);

            CurrencyItem currencyItem = currency.GetComponent<CurrencyItem>();
            currencyItem.Setup();
            shopSlots[i].GetComponent<ShopCurrencySlot>().Setup(currencyItem, i++, this);
        }

        return i;
    }*/

    /*private int CreateEffectSlots(int i)
    {
        foreach (GameObject potion in potionsPrefabs)
        {
            GameObject go = Instantiate(slotEffectPrefab, panelSlots.transform);
            shopSlots.Add(go);

            EffectItem effectItem = potion.GetComponent<EffectItem>();
            effectItem.Setup();
            shopSlots[i].GetComponent<ShopEffectSlot>().Setup(effectItem, i++, this);
        }

        return i;
    }*/

    /*public void DeActivateSlot(int index)
    {
        shopSlots[index].gameObject.SetActive(false);
    }*/

    /*
    public void DeActivateEffectSlots()
    {
        List<GameObject> effectSlots = shopSlots.Where(t => t.GetComponent<ShopEffectSlot>()).ToList();

        foreach(GameObject go in effectSlots)
        {
            DeActivateSlot(go.GetComponent<ShopEffectSlot>().Index);
        }
    }*/

    /*public void ApplySoulFragmentPurchase(int price, int soulFragmentsToAdd, int index)
    {
        ShopController.Instance.ApplySoulFragmentPurchase(price, soulFragmentsToAdd);
        foreach(GameObject go in shopSlots)
        {
            go.GetComponent<ShopSlot>().CheckButtonInteractability();
        }
        DeActivateSlot(index);
    }*/

    /*public void ApplyComboPurchase(int price, BasicComboDefinition combo, int index)
    {
        combo.SetActive(true);
        ShopController.Instance.ApplyComboPurchase(price);
        foreach(GameObject go in shopSlots)
        {
            go.GetComponent<ShopSlot>().CheckButtonInteractability();
        }
        DeActivateSlot(index);
    }*/

    /*public void ApplyEffectPurchase(int price, EffectItem effectItem, int index)
    {
        effectItem.UseItem(ShopController.Instance.GetPlayerController());
        ShopController.Instance.ApplyEffectPurchase(price);
        foreach(GameObject go in shopSlots)
        {
            go.GetComponent<ShopSlot>().CheckButtonInteractability();
        }
        DeActivateEffectSlots();
    }*/
}
