using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] WeaponModel weaponModel;

    private float actualTimeCharging = 0;
    private bool isCharging = false;

    public void StopCharging()
    {
        isCharging = false;
        actualTimeCharging = 0;
    }

    public IEnumerator StartCharging()
    {
        isCharging = true;
        while(isCharging && actualTimeCharging < weaponModel.maxTimeCharge)
        {
            actualTimeCharging += Time.deltaTime;
            Debug.Log("Time recharging = " + actualTimeCharging);
            yield return new WaitForSeconds(Time.deltaTime);
        }

    }
}
