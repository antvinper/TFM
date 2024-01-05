using CompanyStats;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InGameHUD : MonoBehaviour
{
    private InGameHUD instance;
    private bool isPauseMenuOpen = false;

    [SerializeField] protected EventSystem eventSystem;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private InputActionReference pauseReference;
    [SerializeField] private InputActionReference submitReference;
    [SerializeField] private GameObject pauseStartButtonActive;
    [SerializeField] private MovePlayer movePlayer;


    [Header("InGameHUD Parts")]
    [SerializeField] private GameObject inGameHUD;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image sliderColor;
    [SerializeField] private TextMeshProUGUI attackText;
    [SerializeField] private TextMeshProUGUI defenseText;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI soulsText;

    [Header("Pause Menu Parts")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private TextMeshProUGUI pauseMenu_healthText;
    [SerializeField] private TextMeshProUGUI pauseMenu_attackText;
    [SerializeField] private TextMeshProUGUI pauseMenu_defenseText;
    [SerializeField] private TextMeshProUGUI pauseMenu_speedText;
    [SerializeField] private TextMeshProUGUI pauseMenu_coinsText;
    [SerializeField] private TextMeshProUGUI pauseMenu_soulsText;

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
        pauseReference.action.performed += this.PauseIddle;
    }

    void Update()
    {
        //TODO -> Mover el setupInGameHud, sólo cuando ocurra un cambio en los stats del jugador
        //Tal como está chupa muchos recursos.
        SetUpInGameHUD();
    }
    
    private void PauseIddle(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase.Equals(InputActionPhase.Performed))
        {
            if (isPauseMenuOpen)
            {
                ClosePauseMenu();
            }
            else
            {
                SetUpPauseMenu();
                isPauseMenuOpen = true;
                pauseMenu.SetActive(true);
                inGameHUD.SetActive(false);
                Time.timeScale = 0.0f;
                eventSystem.SetSelectedGameObject(pauseStartButtonActive);
                movePlayer.enabled = false;
            }
        }
    }

    public void SetUpInGameHUD()
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

    public void SetUpPauseMenu()
    {
        pauseMenu_healthText.text = playerController.GetStatValue(StatNames.HEALTH, StatParts.ACTUAL_VALUE) + "/" + playerController.GetStatValue(StatNames.HEALTH, StatParts.ACTUAL_MAX_VALUE);
        pauseMenu_attackText.text = playerController.GetStatValue(StatNames.ATTACK, StatParts.ACTUAL_VALUE).ToString();
        pauseMenu_defenseText.text = playerController.GetStatValue(StatNames.DEFENSE, StatParts.ACTUAL_VALUE).ToString();
        pauseMenu_speedText.text = playerController.GetStatValue(StatNames.SPEED, StatParts.ACTUAL_VALUE).ToString();
        pauseMenu_coinsText.text = playerController.Rupees.Amount.ToString();
        pauseMenu_soulsText.text = playerController.SoulFragments.Amount.ToString();
    }

    public void ClosePauseMenu()
    {
        isPauseMenuOpen = false;
        pauseMenu.SetActive(false);
        inGameHUD.SetActive(true);
        Time.timeScale = 1.0f;
        movePlayer.enabled = true;
    }

    public void GoToMainMenu()
    {
        // Llamar a la funci�n de guardado
        Debug.Log("TODO -> Guardar antes de volver al menu de inicio e ir al menu de inicio...");
        //await SceneManager.LoadMenuScene();
    }

    public void SaveAndClose()
    {
        // Llamar a la funci�n de guardado
        Debug.Log("TODO -> Guardando antes de cerrar el juego...");
        Application.Quit();
    }
}
