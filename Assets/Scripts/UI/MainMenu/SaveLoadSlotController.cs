using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadSlotController : MonoBehaviour
{
    [SerializeField] private SaveLoadSlotView view;

    public void Setup(string time, string slotName)
    {
        view.Setup(time, slotName);
    }
}
