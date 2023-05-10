using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BhutaController : MonoBehaviour
{
    //  ## Teletransporte ##
    /*
     * Crear puntos fijos alrededor del enemigo. A partir de esos puntos, obtener un
     * punto aleatorio.
     * Si ese punto no está chocando con ningún objeto, se teletransporta a ese punto
     * sino, busca otro.
     * 
     * Si pasa por todos los puntos y no puede en ninguno, hay que crear un punto aleatorio dentro
     * de un radio...
     */

    BhutaModel model;
    [SerializeField] int actualFloorColliderInstanceId;

    //List<TeleportPoint> teleportPoints;

    // Start is called before the first frame update
    void Start()
    {
        model = GetComponent<BhutaModel>();
        //teleportPoints = new List<TeleportPoint>(GetComponentsInChildren<TeleportPoint>());

        List<Collider> colliders = new List<Collider>(Physics.OverlapSphere(transform.position, 1));

        foreach (Collider c in colliders)
        {
            if(c.gameObject.GetComponent<RoomBounds>())
            {
                actualFloorColliderInstanceId = c.gameObject.GetInstanceID();
            }
        }

        StartCoroutine(TeleportAfterSeconds());
    }

    /*public void Teleport(int index)
    {
        //int index = Random.Range(0, teleportPoints.Count - 1);

        Vector3 telPos = teleportPoints[index].gameObject.transform.position;

        
        if(Physics.CheckSphere(telPos, 1))
        {
            Debug.Log("No puede!");
        }
        else
        {
            gameObject.transform.position = teleportPoints[index].gameObject.transform.position;
        }
        
    }*/

    public void TeleportInSphereRange(float minDistance, float maxDistance)
    {

        Vector3 currentPosition = transform.position;

        Vector3 randomVector = Random.insideUnitSphere;
        randomVector.Normalize();

        float randomDistance = Random.Range(minDistance, maxDistance);
        Vector3 randomPosition = currentPosition + randomVector * randomDistance;

        bool isRandomPosOk = false;

        int count = 0;
        while(isRandomPosOk == false && count <= 50)
        {
            
            isRandomPosOk = GetRandomPos(minDistance, maxDistance, currentPosition, randomVector, randomDistance, ref randomPosition);

            Debug.Log("#RANDOM POS isRandomPosOk? " +  isRandomPosOk);
            ++count;
            Debug.Log("Times call GetRandomPos: " + count);
        }

        if(count < 50)
        {
            Debug.Log("#RANDOM POS Teleporting...");
            gameObject.transform.position = new Vector3(randomPosition.x, gameObject.transform.position.y, randomPosition.z);
        }
        else
        {
            Debug.Log("No ha sido posible encontrar un punto");
        }
        
    }

    private bool GetRandomPos(float minDistance, float maxDistance, Vector3 currentPosition, Vector3 randomVector, float randomDistance, ref Vector3 randomPosition)
    {
        int count = 0;
        bool isRandomPosOk = false;

        while (Vector3.Distance(randomPosition, currentPosition) < minDistance && count <= 50)
        {
            Debug.Log("Times Realculating random pos: " + count++);
            randomVector = Random.insideUnitSphere;
            randomVector.Normalize();
            randomDistance = Random.Range(minDistance, maxDistance);
            randomPosition = currentPosition + randomVector * randomDistance;
        }

        if(Physics.CheckSphere(randomPosition, 1))
        {
            List<Collider> colliders = new List<Collider>(Physics.OverlapSphere(randomPosition, 1));

            //Diferente de sí mismo y de actualfloor a false

            //if(colliders.Count == 1 && colliders[0].gameObject.GetInstanceID().Equals(actualFloorColliderInstanceId))
                if(colliders[0].gameObject.GetInstanceID().Equals(actualFloorColliderInstanceId))
            {
                isRandomPosOk = true;
                Debug.Log("#RANDOM POS " + colliders[0].gameObject.name);
            }
        }

        Debug.Log("#RANDOM POS " + isRandomPosOk);
        return isRandomPosOk;
    }

    //ERASE
    IEnumerator TeleportAfterSeconds()
    {
        for(int i = 0; i < 100; ++i)
        {
            yield return new WaitForSeconds(3);
            //Teleport(i);

            //TODO
            //Ver esto bien calculando max con las distancias a las paredes
           // TeleportInSphereRange(5f, 15f);
            TeleportInSphereRange(1.5f, 5f);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
