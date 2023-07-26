using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Clase para m�todos gen�ricos en cualquier Controlador de enemigo.
 */
public class EnemyController : Characters.CharacterController
{
    /**
     * TODO
     * No s� si vale la pena separar enemy y player models
     */
    //[SerializeField] private EnemyMutableModel model;
    

    public override float GetMyRealDamage()
    {
        throw new System.NotImplementedException();
    }

    public override void ProcessDamage(int value)
    {
        StatModificator statModificator = new StatModificator(StatsEnum.HEALTH, value, false, false);
        model.PerformRealHealthChange(statModificator);
        //model.Health -= value;

        Debug.Log("I have received a damage of " + value);
        Debug.Log("Actual life = " + model.GetStat(StatsEnum.HEALTH));
    }

    /*public override void ProcessEffect(CharacterMutableModel attacker)
    {
        Debug.Log(attacker.Attack);
    }*/
}
