using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RewardPanel : MonoBehaviour
{
    [SerializeField] private RewardsEnum rewardType;
    [SerializeField] private RewardsUiController rewardsUiController;
    [SerializeField] private InputActionReference submitReference;
    [SerializeField] private Button button;

    public RewardsEnum RewardType { get => rewardType; }

    private void Start()
    {
        submitReference.action.performed += this.PerformAction;
        button.onClick.AddListener(async delegate
        {
            this.PerformAction();
        });
        Debug.Log("#REWARD Enable submit action of " + rewardType);
    }

    private void OnDisable()
    {
        submitReference.action.performed -= this.PerformAction;
        Debug.Log("#REWARD Disable submit action");
    }

    private void PerformAction()
    {
        Debug.Log("#REWARD Reward Set to " + rewardType);
        rewardsUiController.SetReward(rewardType);
    }

    private void PerformAction(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase.Equals(InputActionPhase.Performed))
        {
            Debug.Log("#REWARD Reward Set to " + rewardType);
            rewardsUiController.SetReward(rewardType);
        }
    }

    public void SetSelectedButton(EventSystem eventSystem)
    {
        eventSystem.SetSelectedGameObject(button.gameObject);
    }
}
