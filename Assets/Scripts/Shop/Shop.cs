using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
public class Shop: MonoBehaviour
{
    //Lista de pociones 25%, 50%, 75%
    [SerializeField] private List<GameObject> potions;
    //Lista de combos. Est�n todas las listas.
    //En funci�n de qu� arma tenga el personaje, seleccionar� una lista u otra
    //Y de esa lista, s�lo seleccionar� los combos no activados.
    [SerializeField] private List<BasicComboDefinition> swordCombos;
    [SerializeField] private List<BasicComboDefinition> chackramCombos;
    [SerializeField] private List<BasicComboDefinition> combosInShop;

    //Fragmentos de alma. Entre 1 y 3
    [SerializeField] private int soulFragmentPricePerUnit;
    [SerializeField] private int minSoulFragment;
    [SerializeField] private int maxSoulFragment;


    public void CreateShop(WeaponController weaponController)
    {
        int soulFragments = (Random.Range(minSoulFragment, maxSoulFragment + 1));
        int price = soulFragments * soulFragmentPricePerUnit;

        if(weaponController is SwordController)
        {
            combosInShop = swordCombos;
        } else if(weaponController is ChackramController)
        {
            combosInShop = chackramCombos;
        }
    }
}
