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
    //[SerializeField] private EnemyMutableModel Model;

    private void Start()
    {
        //SetModel();
    }

    protected async Task SetModel(CharacterMutableModel model)
    {
        this.model = model;
        this.model.Setup(statsDefinitions);
        //model = new EnemyMutableModel();
        //model.Setup(statsDefinitions);
    }

    public override float GetMyRealDamage()
    {
        throw new System.NotImplementedException();
    }

    public override void ChangeRealHealth(StatModificator statModificator)
    {
        if(statModificator.Value < 0)
        {
            this.ProcessDamage(statModificator);
        }
        else
        {
            Debug.Log("TODO -> Heal");
        }
        /*statModificator = model.PerformRealHealthChange(statModificator);

        if (!statModificator.IsAlive)
        {
            Destroy(transform.gameObject);
        }*/
    }

    public override void ProcessDamage(StatModificator statModificator)
    {
        //StatModificator statModificator = new StatModificator(StatsEnum.HEALTH, value, false, false);
        statModificator = model.PerformRealHealthChange(statModificator);
        //model.Health -= value;

        Debug.Log("I have received a damage of " + statModificator.Value);
        Debug.Log("Actual life = " + model.GetStatValue(StatsEnum.HEALTH));
        if (!statModificator.IsAlive)
        {
            Destroy(transform.gameObject);
        }
    }

    /*public override void ProcessEffect(CharacterMutableModel attacker)
    {
        Debug.Log(attacker.Attack);
    }*/
}
