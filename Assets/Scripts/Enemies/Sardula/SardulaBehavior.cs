using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SardulaBehavior : MonoBehaviour
{
    float timer = 0.0f;
    [SerializeField] Transform player;
    [SerializeField] GameObject centerZoneObject;
    [SerializeField] float dist = 0.0f;
    CenterAreaZone zone;
    // Start is called before the first frame update
    void Start()
    {
        zone = centerZoneObject.GetComponent<CenterAreaZone>();
    }

    // Update is called once per frame
    void Update()
    {
        LookAtTarget(player.position);
        dist = Vector3.Distance(player.position, transform.position);
        timer += Time.deltaTime;
        if ((int)timer == 4)
        {
            action();
            timer = 0.0f;
        }
    }

    void action()
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
        if (action == "center") 
        {
            Debug.Log(action);
            transform.position = new Vector3(centerZoneObject.transform.position.x, transform.position.y, centerZoneObject.transform.position.z);
        }
        else if (action == "dash")
        {
            Debug.Log(action);
            transform.position = new Vector3(player.position.x, transform.position.y, player.position.z);
        }
        else if (action == "slash")
        {
            Debug.Log(action);
        }
        else if (action == "stun")
        {
            Debug.Log(action);
        }
    }
}
