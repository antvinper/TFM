using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadSlotController : MonoBehaviour
{
    [SerializeField] private SaveLoadSlotView view;

    public void Setup(GameMinModel minModel, int n)
    {
        view.Setup(minModel, n);
    }
}
