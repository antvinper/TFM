using CompanyStats;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameHUD : MonoBehaviour
{
    private InGameHUD instance;

    [SerializeField] private PlayerController playerController;

    [Header("HUD Parts")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image sliderColor;
    [SerializeField] private TextMeshProUGUI attackText;
    [SerializeField] private TextMeshProUGUI defenseText;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI soulsText;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        
        // Asignar el player model dinamicamente? Si no, se asigna a mano en todas las escenas
        //Debug.Log("Is there a player controller?: " + (playerController == null ? "No" : "Yes"));
    }

    void Update()
    {
        SetUpHUD();
    }

    public void SetUpHUD()
    {
        healthSlider.value = playerController.GetStatValue(StatNames.HEALTH, StatParts.ACTUAL_VALUE);
        healthSlider.maxValue = playerController.GetStatValue(StatNames.HEALTH, StatParts.ACTUAL_MAX_VALUE);

        float aux = healthSlider.value / healthSlider.maxValue;
        if (aux > 0.5f)
        {
            sliderColor.color = new Color(0, 255, 0, 255);
        }
        else if (aux < 0.5f && aux > 0.2f)
        {
            sliderColor.color = new Color(255, 255, 0, 255);
        }
        else
        {
            sliderColor.color = new Color(255, 0, 0, 255);
        }

        attackText.text = playerController.GetStatValue(StatNames.ATTACK, StatParts.ACTUAL_VALUE).ToString();
        defenseText.text = playerController.GetStatValue(StatNames.DEFENSE, StatParts.ACTUAL_VALUE).ToString();
        speedText.text = playerController.GetStatValue(StatNames.SPEED, StatParts.ACTUAL_VALUE).ToString();
        coinsText.text = playerController.Rupees.Amount.ToString();
        soulsText.text = playerController.SoulFragments.Amount.ToString();
    }
}
