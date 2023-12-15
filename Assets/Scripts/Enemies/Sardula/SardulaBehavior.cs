using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SardulaBehavior : MonoBehaviour
{
    private float timer = 0.0f;
    private Animator anim;
    private string doingAction;
    private bool isAction;
    private Vector3 actionPosition;
    private float timeToReachTarget, t;
    [SerializeField] Transform player;
    [SerializeField] GameObject centerZoneObject;
    [SerializeField] float dist = 0.0f;
    CenterAreaZone zone;
    // Start is called before the first frame update
    void Start()
    {
        zone = centerZoneObject.GetComponent<CenterAreaZone>();
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

            if (doingAction == "slash" && timer > 1.0f && timer <= 1.3f)
            {
                Debug.Log("Estoy haciendo Slash");
            }
            else if(doingAction == "slash" && timer >= 2.0f)
            {
                EndAction();
            }

            if (doingAction == "center" && timer > 0.3f && timer <= 1.0f)
            {
                Debug.Log("Estoy volviendo al centro");
                timeToReachTarget = 0.7f;
                t += Time.deltaTime / timeToReachTarget;
                actionPosition = new Vector3(centerZoneObject.transform.position.x, transform.position.y, centerZoneObject.transform.position.z);
                transform.position = Vector3.Lerp(transform.position, actionPosition, t);
            }
            else if (doingAction == "center" && timer >= 1.4f)
            {
                EndAction();
            }

            if (doingAction == "dash" && timer > 0.3f && timer <= 1.3f)
            {
                Debug.Log("Estoy haciendo Dash");
                timeToReachTarget = 1.0f;
                t += Time.deltaTime / timeToReachTarget;
                transform.position = Vector3.Lerp(transform.position, actionPosition, t);
            }
            else if (doingAction == "dash" && timer >= 2.0f)
            {
                EndAction();
            }

            if (doingAction == "stun" && timer > 2.0f && timer <= 2.1f)
            {
                Debug.Log("Estoy haciendo Stun");
            }
            else if (doingAction == "stun" && timer >= 2.7f)
            {
                EndAction();
            }

            if (doingAction == "beam" && timer > 0.7f && timer <= 2.3f)
            {
                Debug.Log("Estoy haciendo Beam");
            }
            else if (doingAction == "beam" && timer >= 3.7f)
            {
                EndAction();
            }
        }
    }

    void ChooseAction()
    {
        zone = centerZoneObject.GetComponent<CenterAreaZone>();
        if (!zone.player && !zone.boss)
        {
            if (dist > 10)
            {
                RandomBool("center", "beam");
            }
            else
            {
                RandomBool("center", "slash");
            }
        }
        else if(zone.player && zone.boss) 
        {
            RandomBool("stun", "slash");
        }
        else
        {
            RandomBool("dash", "beam");
        }

    }

    void LookAtTarget(Vector3 target)
    {
        target.y = transform.position.y;
        transform.LookAt(target);
    }

    void RandomBool(string action1, string action2)
    {
        if (Random.value >= 0.5)
        {
            CallAction(action1);
        }
        else
        {
            CallAction(action2);
        }
    }
    
    void CallAction(string action)
    {
        anim.SetTrigger(action);
        Debug.Log(action);
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
        Debug.Log("Fin de la accion");
    }
}

