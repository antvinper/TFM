using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Clase para métodos genéricos en cualquier Controlador de enemigo.
 */
public class EnemyController : Characters.CharacterController
{
    /**
     * TODO
     * No sé si vale la pena separar enemy y player models
     */
    //[SerializeField] private EnemyMutableModel model;
    

    public override float GetMyRealDamage()
    {
        throw new System.NotImplementedException();
    }

    public override void ProcessDamage(float value)
    {
        model.Health -= value;

        Debug.Log("I have received a damage of " + value);
        Debug.Log("Actual life = " + model.Health);
    }

    /*public override void ProcessEffect(CharacterMutableModel attacker)
    {
        Debug.Log(attacker.Attack);
    }*/
}
