using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerModel model;
    

    public void TakeDamage(int damage)
    {
        int realDamage = damage - model.Defense;
        int finalDamage = realDamage < 0 ? 0 : realDamage;

        model.Life -= finalDamage;

        if(model.Life <= 0)
        {
            //TODO
            Debug.Log("Dead");
        }
    }

    public void GetInput(InputAction.CallbackContext callbackContext)
    {
        if(callbackContext.phase.Equals(InputActionPhase.Performed))
        {
            if(callbackContext.action.activeControl.ToString().Contains("buttonWest"))
            {
                //Debug.Log("West or X Pressed");
                //TODO
            }
            if(callbackContext.action.activeControl.ToString().Contains("buttonNorth"))
            {
                StartCoroutine(model.activeWeapon.StartCharging());
            }
            if(callbackContext.action.activeControl.ToString().Contains("rightTrigger"))
            {
                //Debug.Log("Right or RT Pressed");
                //TODO
            }
            if(callbackContext.action.activeControl.ToString().Contains("buttonEst"))
            {
                //Debug.log("Dash");
                //TODO
            }
        }

        if(callbackContext.phase.Equals(InputActionPhase.Canceled))
        {
            if (callbackContext.action.activeControl.ToString().Contains("buttonNorth"))
            {
                model.activeWeapon.StopCharging();
            }
        }
    }
}
