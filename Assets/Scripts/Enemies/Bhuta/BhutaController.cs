using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BhutaController : EnemyController<BhutaModel>
{
    void Start()
    {
        model = GetComponent<BhutaModel>();
        model.SetTeleportPoints();

        List<Collider> colliders = new List<Collider>(Physics.OverlapSphere(transform.position, 1));

        foreach (Collider c in colliders)
        {
            if(c.gameObject.GetComponent<RoomBounds>())
            {
                model.floorCollider = c.gameObject.GetComponent<BoxCollider>();
                model.actualFloorColliderInstanceId = c.gameObject.GetInstanceID();
            }
        }
    }

    public void Teleport()
    {
        Vector3 randomPosition = transform.position;
        bool isRandomPosOk = TrySetRandomPos(ref randomPosition);

        if(isRandomPosOk)
        {
            Debug.Log("#RANDOM POS Teleporting...");
            gameObject.transform.position = new Vector3(randomPosition.x, gameObject.transform.position.y, randomPosition.z);
        }
        else
        {
            Debug.Log("#RANDOM POS No ha sido posible encontrar un punto");
        }
        
    }

    private bool TrySetRandomPos(ref Vector3 randomPosition)
    {
        bool isRandomPosOk = false;

        int maxSize = model.teleportPoints.Count; 
        int[] numeros = new int[maxSize];
        List<int> numerosPosibles = Enumerable.Range(0, maxSize).ToList();
        System.Random rnd = new System.Random();

        for (int i = 0; i < maxSize; i++)
        {
            int indice = rnd.Next(numerosPosibles.Count-1);
            numeros[i] = numerosPosibles[indice];
            numerosPosibles.RemoveAt(indice);
        }

        foreach(int n in numeros)
        {
            Vector3 telPos = model.teleportPoints[n].gameObject.transform.position;
            if (Physics.CheckSphere(telPos, 1))
            {
                List<Collider> colliders = new List<Collider>(Physics.OverlapSphere(telPos, 0.5f));
                if (colliders.Count() == 1 && colliders[0].gameObject.GetInstanceID().Equals(model.actualFloorColliderInstanceId))
                {
                    isRandomPosOk = true;
                    randomPosition = telPos;
                    break;
                }
            }
        }

        return isRandomPosOk;
    }
}
