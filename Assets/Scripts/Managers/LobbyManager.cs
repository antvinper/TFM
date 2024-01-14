using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] private Button startRunButton;
    [SerializeField] private Button meditateButton;

    private static LobbyManager instance;
    public static LobbyManager Instance => instance;

    private void Awake()
    {
        instance = this;
    }



    private void Start()
    {
        Debug.Log("Starting LobbyManager...");
        startRunButton.onClick.AddListener(async delegate
        {
            DeActivateLevelSelectorCanvas();
            GameManager.Instance.StartRun();
        });
        meditateButton.onClick.AddListener(async delegate
        {
            DeActivateLevelSelectorCanvas();
            Debug.Log("Starting meditate...");
        });
    }

    private void DeActivateLevelSelectorCanvas()
    {
        startRunButton.GetComponentInParent<Canvas>().transform.gameObject.SetActive(false);
    }
}

