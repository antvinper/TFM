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
        if (Input.GetMouseButtonDown(0))
        {
            moverEspada.Play();
            playerController.DoCombo(ButtonsXbox.X);
        }
    }

    public void GetInput(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase.Equals(InputActionPhase.Performed))
        {
            if (callbackContext.action.activeControl.ToString().Contains("buttonWest"))
            {
                moverEspada.Play();
                playerController.DoCombo(ButtonsXbox.X);

                //Debug.Log("West or X Pressed");
                //TODO
            }
            if (callbackContext.action.activeControl.ToString().Contains("buttonNorth"))
            {
                moverEspada.Play();
                playerController.DoCombo(ButtonsXbox.Y);
                playerController.StartCharging();
            }
            if (callbackContext.action.activeControl.ToString().Contains("rightTrigger"))
            {
                moverEspada.Play();
                playerController.DoCombo(ButtonsXbox.RT);
                Debug.Log("Right or RT Pressed");
                //TODO
            }
            if (callbackContext.action.activeControl.ToString().Contains("buttonEast"))
            {
                //moverEspada.Play();
                playerController.DoDash();
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
}
