using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RewardsUiController : MonoBehaviour
{
    [SerializeField] RewardPanel rupeesPanel;
    [SerializeField] RewardPanel soulFragmentsPanel;
    [SerializeField] RewardPanel healPanel;

    private EventSystem eventSystem;
    private bool rewardHasBeenSelected = false;
    public bool RewardHasBeenSelected { get => rewardHasBeenSelected; }
    

    private void Start()
    {
        eventSystem = FindObjectOfType<EventSystem>();
        rupeesPanel.SetSelectedButton(eventSystem);
    }

    

    internal void SetReward(RewardsEnum rewardType)
    {
        GameManager.Instance.NextRoomReward = rewardType;
        rewardHasBeenSelected = true;
    }

    internal void ResetRewardHasBeenSelected()
    {
        rewardHasBeenSelected = false;
    }
}
