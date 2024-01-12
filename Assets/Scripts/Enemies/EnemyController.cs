using CompanyStats;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System;


/*
 * Clase para m�todos gen�ricos en cualquier Controlador de enemigo.
 */
public class EnemyController : CompanyCharacterController
{
    [SerializeField] private GameObject originalRender;
    [SerializeField] private GameObject dissolveRender;
    [SerializeField] private SpawnEffect despawnEffect;

    private RoomController roomController;
    public RoomController RoomController { set => roomController = value; }

    public bool isHit = false;
    Guid gUID;
    public Guid GUID
    {
        get => gUID;
    }
    //protected new EnemyMutableModel model { get; set; }

    /**
     * TODO
     * No s� si vale la pena separar enemy y player models
     */
    //[SerializeField] private EnemyMutableModel Model;

    private void Start()
    {
        gUID = Guid.NewGuid();
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
        if(GetStatValue(StatNames.HEALTH, StatParts.ACTUAL_VALUE) > 0)
        {
            Debug.Log("Health before damage = " + model.GetStatValue(StatNames.HEALTH, StatParts.ACTUAL_VALUE));
            isHit = true;
            bool isAlive = model.ApplyDamage(strike);
            Debug.Log("Applied an attack of: " + strike.FinalValue + " points");
            Debug.Log("Health after damage = " + model.GetStatValue(StatNames.HEALTH, StatParts.ACTUAL_VALUE));

            if (!isAlive)
            {
                DieBehaviour();
            }
            isHit = false;
        }
    }

    public override async Task DieBehaviour()
    {
        Debug.Log("TODO -> Enemy Behaviour when dies.");

        //RoomManager.Instance.OnEnemyKilled(this);
        if(roomController == null)
        {
            roomController = FindObjectOfType<RoomController>();
        }
        else
        {
            roomController.OnEnemyKilled(this);
        }

        await new WaitForSeconds(despawnEffect.spawnEffectTime);
        dissolveRender.SetActive(true);
        originalRender.SetActive(false);
        await new WaitForSeconds(despawnEffect.spawnEffectTime);
        base.DieBehaviour();
    }




    /*public override void ProcessEffect(CharacterMutableModel attacker)
    {
        Debug.Log(attacker.Attack);
    }*/
}
