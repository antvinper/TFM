using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SardulaBehavior : MonoBehaviour
{
    [SerializeField] float timer = 0.0f;
    private Animator anim;
    private string doingAction;
    private bool isAction;
    private Vector3 actionPosition;
    private float timeToReachTarget, t;
    [SerializeField] Transform player;
    [SerializeField] float dist = 0.0f;
    [SerializeField] Transform centerZoneObject;
    [SerializeField] GameObject beamProjectile;
    [SerializeField] Transform beamSpawn;
    [SerializeField] GameObject stompArea;
    [SerializeField] Transform stompSpawn;
    [SerializeField] GameObject dashArea;
    [SerializeField] Transform slashSpawn;
    [SerializeField] GameObject slashProyectile;
    [SerializeField] private int proyectileNumber;
    [SerializeField] GameObject centerProyectile;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        isAction = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Si no esta haciendo una accion, mira al jugador
        if (!isAction)
        {
            actionPosition = player.position;
        }

        LookAtTarget(actionPosition);

        dist = Vector3.Distance(player.position, transform.position);

        timer += Time.deltaTime;
        if ((int)timer == 4)
        {
            ChooseAction();
            timer = 0.0f;
        }

        if (isAction)
        {

            if (doingAction == "slash" && timer > 1.0f && timer <= 1.03f)
            {
                GameObject tmpObject = Instantiate(slashProyectile, slashSpawn.position, slashSpawn.rotation);
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

            if (doingAction == "stun" && timer > 2.0f && timer <= 2.05f)
            {
                GameObject tmpObject = Instantiate(stompArea, stompSpawn.position, stompSpawn.rotation);
                Destroy(tmpObject, 0.3f);
            }
            else if (doingAction == "stun" && timer >= 2.7f)
            {
                EndAction();
            }

            if (doingAction == "beam" && timer > 0.7f && timer <= 2.3f)
            {
                GameObject tmpObj =Instantiate(beamProjectile,beamSpawn.position, beamSpawn.rotation);
                Destroy(tmpObj, 3);
            }
            else if (doingAction == "beam" && timer >= 3.7f)
            {
                EndAction();
            }
        }
    }

    void ChooseAction()
    {
        CallAction("center");
        /* accion;
        if (dist > 10)
        {
            accion = RandomBool(RandomBool("dash", "beam"), RandomBool("dash", "center"));
            CallAction(accion);
        }
        else
        {
            accion = RandomBool("slash", "stun");
            CallAction(accion);
        }*/
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
        actionPosition = new Vector3(player.position.x, transform.position.y, player.position.z); ;
        doingAction = action;
        isAction = true;
    }

    void EndAction()
    {
        isAction = false;
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
            tmpObj.GetComponent<Rigidbody>().velocity = new Vector3(proyectileMoveDirection.x, 0, proyectileMoveDirection.y);
            Destroy(tmpObj, 3);
            angle += angleStep;
        }
    }

}

