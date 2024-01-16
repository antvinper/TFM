using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

//Este shopSlot tiene que ser una especie de factory
//Puedo comprar efectos, combos y monedas
//Cada slot tiene uno de ellos y al hacer el purchase
//Se har� una cosa u otra.
//Igual con simple herencia se puede.

public class ShopSlot : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] protected Button purchaseButton;
    [SerializeField] protected TextMeshProUGUI purchaseButtonText;
    protected int index;
    public int Index => index;
    protected int price;
    protected bool hasBeenPurchased;

    protected ShopController shopController;

    public void Setup(Item item, ShopController shopController)
    {
        this.price = item.FinalPrice;
        this.name.text = item.Name + item.NameSufix;
        this.description.text = item.Description;
        this.image.sprite = item.Sprite;
        purchaseButton.GetComponentInChildren<TextMeshProUGUI>().text ="Buy " + this.price + "Rp";

        this.shopController = shopController;

        gameObject.SetActive(true);

        CheckButtonInteractability();
    }

    public void Purchase()
    {
        Debug.Log(name.text + ": purchased");
        hasBeenPurchased = true;
    }

    public void CheckButtonInteractability()
    {
        bool canPayit = price <= GameManager.Instance.GetPlayerController().Rupees.Amount;
        if (!canPayit || hasBeenPurchased)
        {
            purchaseButton.interactable = false;
            if (hasBeenPurchased)
            {
                purchaseButtonText.text = "Comprado!";
            }
            
        }
    }

    public bool SetButtonAsFirstSelected(EventSystem eventSystem)
    {
        bool hasBeenSetted = false;
        if (purchaseButton.interactable)
        {
            eventSystem.SetSelectedGameObject(purchaseButton.gameObject);
            hasBeenSetted = true;
        }

        return hasBeenSetted;
    }
}
