using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelView : MonoBehaviour
{
    [SerializeField] protected GameObject firstSlotActive;
    public GameObject FirstSlotActive => firstSlotActive;

    public virtual void Setup<T>(T model) { }
}
