using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PanelController : MonoBehaviour
{
    [SerializeField] protected EventSystem eventSystem;
    [SerializeField] protected PanelView panelView;

    public void SetFirstActiveSlot()
    {
        eventSystem.SetSelectedGameObject(panelView.FirstSlotActive);
    }

    protected void Setup()
    {
        SetFirstActiveSlot();
    }
}
