using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class SaveLoadPanelController : PanelController
{
    /*[SerializeField] private InputActionReference cancelReference;
    [SerializeField] private PanelView saveLoadPanelView;
    [SerializeField] private GameObject container;
    private MainMenuCanvasController mainMenuCanvasController;

    public override void Setup<T>(List<T> models)
    {
        base.Setup();
        FillSlots(models as List<GameMinModel>);
    }

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
        //Setup();
        cancelReference.action.performed += OnCancel;
    }

    private void OnDisable()
    {
        cancelReference.action.performed -= OnCancel;
    }

    public List<SaveLoadSlotController> GetSlot()
    {
        return GetComponentsInChildren<SaveLoadSlotController>().ToList();
    }

    public void FillSlots(List<GameMinModel> savedData)
    {
        List<SaveLoadSlotController> slots = new List<SaveLoadSlotController>(GetSlot());

        for (int i = 0; i < savedData.Count; ++i)
        {
            GameMinModel g = savedData[i];
            slots[i].Setup(g, i);
        }
    }*/

    
}
