using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerModel model;

    public void TakeDamage(int damage)
    {
        int realDamage = damage - model.Defense;
        int finalDamage = realDamage < 0 ? 0 : realDamage;

        model.Life -= finalDamage;

        if(model.Life <= 0)
        {
            //TODO
            Debug.Log("Dead");
        }
    }
}
