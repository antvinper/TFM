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

    private int price;

    public void Setup(Item item)
    {
        this.price = item.Price;
        this.name.text = item.Name;
        this.description.text = item.Description;
        this.image.sprite = item.Sprite;
        purchaseButton.GetComponentInChildren<TextMeshProUGUI>().text ="Buy " + this.price + "Rp";

        gameObject.SetActive(true);

        //boton
        if (price > GameManager.Instance.GetPlayerController().Model.Rupees)
        {
            purchaseButton.interactable = false;
        }
    }

    public void Purchase()
    {
        Debug.Log(name.text + ": purchased");
    }
}
