using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField] PlayerController playerController;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            playerController.DoCombo(ButtonsXbox.X);
            dealingDamage();
        }
        notDealingDamage();
    }

    public void GetInput(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase.Equals(InputActionPhase.Performed))
        {
            if (callbackContext.action.activeControl.ToString().Contains("buttonWest"))
            {
                playerController.DoCombo(ButtonsXbox.X);

                //Debug.Log("West or X Pressed");
                //TODO
            }
            if (callbackContext.action.activeControl.ToString().Contains("buttonNorth"))
            {
                playerController.DoCombo(ButtonsXbox.Y);
                playerController.StartCharging();
            }
            if (callbackContext.action.activeControl.ToString().Contains("rightTrigger"))
            {
                playerController.DoCombo(ButtonsXbox.RT);
                Debug.Log("Right or RT Pressed");
                //TODO
            }
            if (callbackContext.action.activeControl.ToString().Contains("buttonEst"))
            {
                playerController.DoCombo(ButtonsXbox.B);
                Debug.Log("Dash");
                //TODO
            }
        }

        if (callbackContext.phase.Equals(InputActionPhase.Canceled))
        {
            if (callbackContext.action.activeControl.ToString().Contains("buttonNorth"))
            {
                playerController.StopCharging();
            }
        }
    }

    void dealingDamage()
    {
        GetComponentInChildren<SwordController>().atacando = true;

    }

    void notDealingDamage()
    {
        GetComponentInChildren<SwordController>().atacando = false;

    }
}
