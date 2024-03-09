using CompanyStats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SardulaBehavior : EnemyController
{
    [SerializeField] float timer = 0.0f;
    private Animator anim;
    private string doingAction;
    private bool isAction;
    private bool doneAction;
    private Vector3 actionPosition;
    private float timeToReachTarget, t;
    [SerializeField] Transform playerTransform;
    [SerializeField] float dist = 0.0f;
    [SerializeField] Transform centerZoneObject;
    [SerializeField] GameObject beamProjectile;
    [SerializeField] Transform beamSpawn;
    [SerializeField] GameObject stompArea;
    [SerializeField] GameObject dashArea;
    [SerializeField] Transform slashSpawn;
    [SerializeField] GameObject slashProyectile;
    [SerializeField] private int proyectileNumber;
    [SerializeField] GameObject centerProyectile;
    [SerializeField] GameObject secondPhase;
    public bool isSecondPhase;
    private SardulaModel sardulaModel;
    public SardulaModel SardulaModel
    {
        get => sardulaModel;
        set => sardulaModel = value;
    }
    // Start is called before the first frame update

    private void Awake()
    {
        if (secondPhase != null) { secondPhase.SetActive(false); }
    }
    void Start()
    {
        sardulaModel = new SardulaModel();
        this.SetModel(sardulaModel);
        playerTransform = GameManager.Instance.GetPlayerController().GetComponentInParent<CharacterController>().transform;
        stompArea.GetComponent<StompArea>().sardula = this;
        anim = GetComponent<Animator>();
        CallAction("center");
        isAction = true;
        isSecondPhase = false;
        Debug.Log(sardulaModel.GetStatValue(StatNames.HEALTH, StatParts.ACTUAL_VALUE));
    }

    // Update is called once per frame
    void Update()
    {
        //Si no esta haciendo una accion, mira al jugador
        if (!isAction)
        {
            actionPosition = playerTransform.position;
        }

        if ((sardulaModel.GetStatValue(StatNames.HEALTH, StatParts.ACTUAL_VALUE) <= 1250 && secondPhase != null) || (isSecondPhase && secondPhase != null))
        {
            secondPhase.SetActive(true);
        }

        LookAtTarget(actionPosition);

        dist = Vector3.Distance(playerTransform.position, transform.position);

        timer += Time.deltaTime;
        if ((int)timer == 4)
        {
            ChooseAction();
            timer = 0.0f;
        }

        if (isAction)
        {

            if (doingAction == "slash" && timer > 1.0f && !doneAction)
            {
                doneAction = true;
                GameObject tmpObject = Instantiate(slashProyectile, slashSpawn.position, slashSpawn.rotation);
                tmpObject.GetComponent<SlashProjectile>().sardula = this;
                Destroy(tmpObject, 3);
            }
            else if(doingAction == "slash" && timer >= 2.0f)
            {
                EndAction();
            }

            if (doingAction == "center" && timer > 0.3f && timer <= 1.0f)
            {
                timeToReachTarget = 0.7f;
                t += Time.deltaTime / timeToReachTarget;
                actionPosition = new Vector3(centerZoneObject.position.x, transform.position.y, centerZoneObject.position.z);
                transform.position = Vector3.Lerp(transform.position, actionPosition, t);
            }
            else if (doingAction == "center" && timer >= 1.4f)
            {
                SpawnProyectile();
                EndAction();
            }

            if (doingAction == "dash" && timer > 0.3f && timer <= 1.3f)
            {
                dashArea.SetActive(true);
                timeToReachTarget = 1.0f;
                t += Time.deltaTime / timeToReachTarget;
                transform.position = Vector3.Lerp(transform.position, actionPosition, t);
                if(transform.position == actionPosition)
                {
                    dashArea.SetActive(false);
                }
            }
            else if (doingAction == "dash" && timer >= 2.0f)
            {
                timer = 3.9f;
                EndAction();
            }

            if (doingAction == "stun" && timer > 2.0f)
            {
                if (timer > 2.03)
                {
                    stompArea.SetActive(false);
                }
                else
                {
                    stompArea.SetActive(true);
                }
            }
            else if (doingAction == "stun" && timer >= 2.7f)
            {
                EndAction();
            }

            if (doingAction == "beam" && timer > 0.7f)
            {
                if (timer >= 2.3f)
                {
                    beamProjectile.SetActive(false);
                }
                else
                {
                    beamProjectile.SetActive(true);
                }
            }
            else if (doingAction == "beam" && timer >= 3.7f)
            {
                EndAction();
            }
        }
    }

    void ChooseAction()
    {
        string accion;
        if (dist > 10)
        {
            accion = RandomBool(RandomBool("dash", "beam"), RandomBool("slash", "center"));
            CallAction(accion);
        }
        else
        {
            accion = RandomBool(RandomBool("slash", "beam"), RandomBool("stun","center"));
            CallAction(accion);
        }
    }

    void LookAtTarget(Vector3 target)
    {
        target.y = transform.position.y;
        transform.LookAt(target);
    }

    string RandomBool(string action1, string action2)
    {
        if (Random.value >= 0.5)
        {
           return action1;
        }
        else
        {
            return action2;
        }
    }
    
    void CallAction(string action)
    {
        anim.SetTrigger(action);
        actionPosition = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z); ;
        doingAction = action;
        isAction = true;
    }

    void EndAction()
    {
        isAction = false;
        doneAction = false;
        doingAction = "none";
        timeToReachTarget = 0;
        t = 0;
    }

    private void SpawnProyectile()
    {
        float angleStep = 360f / proyectileNumber;
        float angle = 0f;

        for (int i = 1; i <= proyectileNumber; i++)
        {
            float projectileDirXPosition = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * 1F;
            float projectileDirYPosition = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * 1F;

            Vector3 proyectileVector = new Vector3(projectileDirXPosition, projectileDirYPosition, 0 );
            Vector3 proyectileMoveDirection = (proyectileVector - transform.position).normalized * 200;
            GameObject tmpObj = Instantiate(centerProyectile,transform.position,Quaternion.identity);
            tmpObj.GetComponent<CenterProjectile>().sardula = this;
            tmpObj.GetComponent<Rigidbody>().velocity = new Vector3(proyectileMoveDirection.x, 0, proyectileMoveDirection.y);
            Destroy(tmpObj, 3);
            angle += angleStep;
        }
    }

}

