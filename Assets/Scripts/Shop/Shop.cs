using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
public class Shop: MonoBehaviour
{
    [SerializeField] Canvas shoppingCanvas;
    [SerializeField] GameObject panelSlots;
    [SerializeField] GameObject slotEffectPrefab;
    [SerializeField] GameObject slotCurrencyPrefab;
    [SerializeField] GameObject slotComboPrefab;

    [SerializeField] List<GameObject> shopSlots;
    [SerializeField] private List<GameObject> potionsPrefabs;
    [SerializeField] private List<GameObject> currencyPrefabs;

    [SerializeField] private List<BasicComboDefinition> swordCombos;
    [SerializeField] private List<BasicComboDefinition> chackramCombos;
    [SerializeField] private List<BasicComboDefinition> combosInShop;

    private PlayerController playerController;
    public PlayerController PlayerController
    {
        get => playerController;
    }


    public void CreateShop(PlayerController playerController)
    {
        this.playerController = playerController;
        WeaponController weaponController = playerController.weaponController;


        if(weaponController is SwordController)
        {
            combosInShop = swordCombos;
        } else if(weaponController is ChackramController)
        {
            combosInShop = chackramCombos;
        }

        shopSlots = new List<GameObject>();
        int i = 0;
        i = CreateEffectSlots(i);

        
        i = CreateCurrencySlots(i);

        CreateComboSlot(i);

        shoppingCanvas.gameObject.SetActive(true);
    }

    private void CreateComboSlot(int i)
    {
        GameObject go = Instantiate(slotComboPrefab, panelSlots.transform);
        shopSlots.Add(go);

        int indexCombo = Random.Range(0, combosInShop.Count);
        BasicComboDefinition bcd = combosInShop[indexCombo];
        ComboItem comboItem = new ComboItem();
        comboItem.Setup(bcd);
        shopSlots[i].GetComponent<ShopComboSlot>().Setup(comboItem, i++, this);
    }

    private int CreateCurrencySlots(int i)
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
    }

    private int CreateEffectSlots(int i)
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
    }

    public void DeActivateSlot(int index)
    {
        shopSlots[index].gameObject.SetActive(false);
    }

    public void ApplyPurchase(int price)
    {
        ShopManager.Instance.ApplyPurchase(price);
        foreach(GameObject go in shopSlots)
        {
            go.GetComponent<ShopSlot>().CheckButtonInteractability();
        }
    }
}
