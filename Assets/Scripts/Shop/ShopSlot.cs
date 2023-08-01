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
    [SerializeField] private Button purchaseButton;

    private int price;

    public void Setup(string name, string description, int price, Sprite image)
    {
        this.price = price;

        this.name.text = name;
        purchaseButton.GetComponentInChildren<TextMeshProUGUI>().text ="Buy " + this.price + "Rp";
        this.description.text = description;
        this.image.sprite = image;

        gameObject.SetActive(true);
    }

    public void Purchase()
    {
        Debug.Log(name.text + ": purchased");
    }
}
