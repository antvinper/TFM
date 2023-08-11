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
    [SerializeField] private TextMeshProUGUI characterLevelText;
    [SerializeField] private TextMeshProUGUI soulFragmentsText;
    [SerializeField] private Button button;
    [SerializeField] private GameObject slotContainer;

    public void Setup(GameMinModel minModel, int n)
    {
        emptyText.gameObject.SetActive(false);

        timeText.text = "Total time: " + minModel.totalPlayTime.Hours + ":" + minModel.totalPlayTime.Minutes;
        slotNameText.text = "Slot " + n;
        characterLevelText.text = "Level: " + minModel.characterLevel;
        soulFragmentsText.text = "Soul fragments: " + minModel.soulFragments;

        slotContainer.SetActive(true);

        Debug.Log("Setting up: " + this.gameObject.name);
    }
}
