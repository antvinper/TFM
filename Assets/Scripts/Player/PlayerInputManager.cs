using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] private AudioSource moverEspada;

    private void Update()
    {
        /*if (Input.GetMouseButtonDown(0))
        {
            moverEspada.Play();
            playerController.DoCombo(ButtonsXbox.X);
        }*/
    }

    public void GetInput(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase.Equals(InputActionPhase.Performed))
        {
            string controlPerformed = callbackContext.action.activeControl.ToString();
            if (controlPerformed.Contains("buttonWest") || controlPerformed.Contains("Keyboard/h"))
            {
                moverEspada.Play();
                playerController.DoCombo(ButtonsXbox.X);

                //Debug.Log("West or X Pressed");
                //TODO
            }
            if (controlPerformed.Contains("buttonNorth") || controlPerformed.Contains("Keyboard/u"))
            {
                moverEspada.Play();
                playerController.DoCombo(ButtonsXbox.Y);
                playerController.StartCharging();
            }
            if (controlPerformed.Contains("rightTrigger") || controlPerformed.Contains("Keyboard/rightShift"))
            {
                moverEspada.Play();
                playerController.DoCombo(ButtonsXbox.RT);
                //Debug.Log("Right or RT Pressed");
                //TODO
            }
            if (controlPerformed.Contains("buttonEast") || controlPerformed.Contains("Keyboard/k"))
            {
                //moverEspada.Play();
                //playerController.DoDash();
                playerController.DoCombo(ButtonsXbox.B);
                //Debug.Log("Dash");
                //TODO
            }
        }

        if (callbackContext.phase.Equals(InputActionPhase.Canceled))
        {
            string controlPerformed = callbackContext.action.activeControl.ToString();
            if (controlPerformed.Contains("buttonNorth") || controlPerformed.Contains("Keyboard/u"))
            {
                playerController.StopCharging();
            }
        }
    }
}
