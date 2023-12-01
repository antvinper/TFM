using CompanyStats;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


/*
 * Clase para m�todos gen�ricos en cualquier Controlador de enemigo.
 */
public class EnemyController : CompanyCharacterController
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
        this.model.Setup(characterStatsDefinition);
        base.Setup(this.model);
        //model = new EnemyMutableModel();
        //model.Setup(statsDefinitions);
    }

    /*public override float GetMyRealDamage()
    {
        throw new System.NotImplementedException();
    }*/

    /*public override void ChangeRealHealth(StatModificator statModificator)
    {
        if(statModificator.Value < 0)
        {
            this.ProcessDamage(statModificator);
        }
        else
        {
            Debug.Log("TODO -> Heal");
        }
    }*/

    public override void ApplyDamage(Strike strike)
    {
        Debug.Log("Health before damage = " + model.GetStatValue(StatNames.HEALTH, StatParts.ACTUAL_VALUE));
        bool isAlive = model.ApplyDamage(strike);
        Debug.Log("Applied an attack of: " + strike.FinalValue + " points");
        Debug.Log("Health after damage = " + model.GetStatValue(StatNames.HEALTH, StatParts.ACTUAL_VALUE));

        if (!isAlive)
        {
            Debug.Log("TODO -> Behaviour when dies."); 
            if (transform.tag.Equals("Enemy"))
            {
                RoomManager.Instance.OnEnemyKilled(this);
            }
            Destroy(transform.gameObject);
            
        }
    }

    /*public override void ProcessEffect(CharacterMutableModel attacker)
    {
        Debug.Log(attacker.Attack);
    }*/
}
