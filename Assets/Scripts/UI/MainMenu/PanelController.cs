using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class PanelController : MonoBehaviour
{
    [SerializeField] protected EventSystem eventSystem;
    [SerializeField] protected PanelView panelView;

    public void SetFirstActiveSlot()
    {
        eventSystem.SetSelectedGameObject(panelView.FirstSlotActive);
    }

    public virtual void Setup() 
    {
        SetFirstActiveSlot();
    }

    public virtual void Setup<T>(List<T> models)
    {
        SetFirstActiveSlot();
    }

    //public virtual void Setup<T>(List<T> models) { }
}
