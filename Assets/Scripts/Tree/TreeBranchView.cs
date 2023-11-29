using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TreeBranchView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statNameText;
    [SerializeField] private TextMeshProUGUI slotsActivesText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Button incrementTreeBranchButton;

    public Button IncrementTreeBranchButton
    {
        get => incrementTreeBranchButton;
    }

    public void Setup(TreeSlot slot)
    {
        statNameText.text = slot.Effect.EffectName;
        RefreshUI(slot);
    }

    public void RefreshUI(TreeSlot slot)
    {
        slotsActivesText.text = slot.ActualActives + "/" + slot.MaxActives;
        priceText.text = slot.GetActualPrice() + "$";
    }

}
