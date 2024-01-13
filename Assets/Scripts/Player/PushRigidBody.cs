using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushRigidBody : MonoBehaviour
{
    private float pushPower = 2.0f;
    private float targetMass;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //Almacena el rigidbody del objeto con el que esta colisionando el jugador
        Rigidbody rb = hit.collider.attachedRigidbody;

        if (rb == null || rb.isKinematic)
        {
            return;
        } 
        
        //Si cae encima de un objeto no se hace nada
        if (hit.moveDirection.y < -0.3)
        {
            return;
        }

        targetMass = rb.mass;

        //La dirección hacia donde empuja
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        rb.velocity = pushDir * pushPower / targetMass;
    }
}
