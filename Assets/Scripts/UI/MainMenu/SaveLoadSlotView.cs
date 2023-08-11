using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadSlotView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI emptyText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI slotNameText;
    [SerializeField] private Button button;
    [SerializeField] private GameObject slotContainer;

    public void Setup(string time, string slotName)
    {
        Debug.Log("Setting up: " + this.gameObject.name);
    }
}
