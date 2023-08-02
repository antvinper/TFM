using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Este shopSlot tiene que ser una especie de factory
//Puedo comprar efectos, combos y monedas
//Cada slot tiene uno de ellos y al hacer el purchase
//Se hará una cosa u otra.
//Igual con simple herencia se puede.

public class ShopSlot : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] protected Button purchaseButton;
    protected int index;
    public int Index => index;
    protected int price;

    protected Shop shop;

    public void Setup(Item item, Shop shop)
    {
        this.price = item.FinalPrice;
        this.name.text = item.Name + item.NameSufix;
        this.description.text = item.Description;
        this.image.sprite = item.Sprite;
        purchaseButton.GetComponentInChildren<TextMeshProUGUI>().text ="Buy " + this.price + "Rp";

        this.shop = shop;

        gameObject.SetActive(true);

        CheckButtonInteractability();
    }

    public void Purchase()
    {
        Debug.Log(name.text + ": purchased");
    }

    public void CheckButtonInteractability()
    {
        if (price > GameManager.Instance.GetPlayerController().Model.Rupees)
        {
            purchaseButton.interactable = false;
        }
    }
}
