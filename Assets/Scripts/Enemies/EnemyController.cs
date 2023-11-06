using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


/*
 * Clase para m�todos gen�ricos en cualquier Controlador de enemigo.
 */
public class EnemyController : Characters.CharacterController
{
    //protected new EnemyMutableModel model { get; set; }

    /**
     * TODO
     * No s� si vale la pena separar enemy y player models
     */
    //[SerializeField] private EnemyMutableModel model;

    private void Start()
    {
        SetModel();
    }

    private async Task SetModel()
    {
        model = new EnemyMutableModel();
        model.Setup(statsDefinitions);
    }

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
        Debug.Log("Actual life = " + model.GetStatValue(StatsEnum.HEALTH));
    }

    /*public override void ProcessEffect(CharacterMutableModel attacker)
    {
        Debug.Log(attacker.Attack);
    }*/
}
