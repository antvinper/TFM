using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] PlayerModel model;
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

    public void DoCombo(ButtonsXbox buttonPressed)
    {
        model.activeWeapon.DoCombo(buttonPressed);
    }

    public void StartCharging()
    {
        StartCoroutine(model.activeWeapon.StartCharging());
    }

    public void StopCharging()
    {
        model.activeWeapon.StopCharging();
    }
}
