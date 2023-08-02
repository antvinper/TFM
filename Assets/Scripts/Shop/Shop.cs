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
    //Lista de pociones 25%, 50%, 75%
    [SerializeField] private List<GameObject> potionsPrefabs;
    //Lista de posibles currencys
    [SerializeField] private List<GameObject> currencyPrefabs;
    //Lista de combos. Están todas las listas.
    //En función de qué arma tenga el personaje, seleccionará una lista u otra
    //Y de esa lista, sólo seleccionará los combos no activados.
    [SerializeField] private List<BasicComboDefinition> swordCombos;
    [SerializeField] private List<BasicComboDefinition> chackramCombos;
    [SerializeField] private List<BasicComboDefinition> combosInShop;

    //Fragmentos de alma. Entre 1 y 3
    /*[SerializeField] private int soulFragmentPricePerUnit;
    [SerializeField] private int minSoulFragment;
    [SerializeField] private int maxSoulFragment;*/

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

        //Crear resto de slots
        CreateComboSlot(i);

        shoppingCanvas.gameObject.SetActive(true);
    }

    private void CreateComboSlot(int i)
    {
        //TODO
        GameObject go = Instantiate(slotComboPrefab, panelSlots.transform);
        shopSlots.Add(go);

        int indexCombo = Random.Range(0, combosInShop.Count);
        BasicComboDefinition bcd = combosInShop[indexCombo];
        ComboItem comboItem = new ComboItem();
        comboItem.Setup(bcd);
        shopSlots[i].GetComponent<ShopComboSlot>().Setup(comboItem, i++);
    }

    private int CreateCurrencySlots(int i)
    {
        foreach (GameObject currency in currencyPrefabs)
        {
            GameObject go = Instantiate(slotCurrencyPrefab, panelSlots.transform);
            shopSlots.Add(go);

            CurrencyItem currencyItem = currency.GetComponent<CurrencyItem>();
            currencyItem.Setup();
            shopSlots[i].GetComponent<ShopCurrencySlot>().Setup(currencyItem, i++);
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
            shopSlots[i].GetComponent<ShopEffectSlot>().Setup(effectItem, i++);
        }

        return i;
    }

    public void DeActivateSlot(int index)
    {
        shopSlots[index].gameObject.SetActive(false);
    }
}
