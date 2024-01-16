using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopModel : MonoBehaviour
{
    [SerializeField] private List<GameObject> potionsPrefabs;
    [SerializeField] private List<GameObject> currencyPrefabs;
    [SerializeField] private List<BasicComboDefinition> combosInShop;

    /*private readonly int effectSlotsIndex = 0;
    private readonly int currencySlotsIndex = 1;
    private readonly int comboSlotsIndex = 2;*/

    internal List<GameObject> PotionsPrefabs { get => potionsPrefabs; }
    internal List<GameObject> CurrencyPrefabs { get => currencyPrefabs; }
    internal List<BasicComboDefinition> CombosInShop { get => combosInShop; }
    /*internal int EffectSlotsIndex { get => effectSlotsIndex; }
    internal int CurrencySlotsIndex { get => currencySlotsIndex; }
    internal int ComboSlotsIndex { get => comboSlotsIndex; }*/

    internal void Setup()
    {
        combosInShop = GetInactiveCombos();
    }

    internal List<BasicComboDefinition> GetInactiveCombos()
    {
        return GameManager.Instance.GetPlayerController().GetInactiveCombos();
    }
}
