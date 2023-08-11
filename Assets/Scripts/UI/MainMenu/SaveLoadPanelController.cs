using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class SaveLoadPanelController : PanelController
{
    [SerializeField] private InputActionReference cancelReference;
    [SerializeField] private PanelView saveLoadPanelView;
    private MainMenuCanvasController mainMenuCanvasController;

    private void Start()
    {
        mainMenuCanvasController = GetComponentInParent<MainMenuCanvasController>();
    }

    private void OnCancel(CallbackContext context)
    {
        mainMenuCanvasController.LoadMainMenuPanel();
    }

    private void OnEnable()
    {
        Setup();
        cancelReference.action.performed += OnCancel;
    }

    private void OnDisable()
    {
        cancelReference.action.performed -= OnCancel;
    }

    

}
