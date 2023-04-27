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
            //Debug.Log("Action: " + callbackContext.action);
            //Debug.Log("Action name: " + callbackContext.action.name);
            //Debug.Log("Binding name: " + callbackContext.action.GetBindingDisplayString());
            //Debug.Log("Active control: " + callbackContext.action.activeControl);

            if(callbackContext.action.activeControl.ToString().Contains("buttonWest"))
            {
                //Debug.Log("West or X Pressed");
            }
            if(callbackContext.action.activeControl.ToString().Contains("buttonNorth"))
            {
                Debug.Log("Nortyh or Y Pressed");
                StartCoroutine(model.activeWeapon.StartCharging());
                Debug.Log("HOLAAAAAAAAAA");
            }
            if(callbackContext.action.activeControl.ToString().Contains("rightTrigger"))
            {
                //Debug.Log("Right or RT Pressed");
            }

            Debug.Log("Phase: " + callbackContext.phase);
        }

        if(callbackContext.phase.Equals(InputActionPhase.Canceled))
        {
            if (callbackContext.action.activeControl.ToString().Contains("buttonNorth"))
            {
                Debug.Log("Nortyh or Y Canceled");
                model.activeWeapon.StopCharging();
            }
        }
    }
}
