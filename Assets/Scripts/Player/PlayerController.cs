using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] PlayerMutableModel model;

    public WeaponController weaponController;

    /**
     * TODO
     * borrar este start y ponerlo donde toque
     */
    private void Start()
    {
        Debug.Log(model.Accuracy);
        weaponController = GetComponent<WeaponController>();
        SetModel();
    }

    private async Task SetModel()
    {
        await new WaitForSeconds(1.0f);
        GameManager.Instance.GameData.PlayerModel = model;
    }

    public void ProcessDamage(float value)
    {
        /**
         * TODO
         * Usar dodgeChance, critChance, critDamageMultiplier...
         */
        model.TakeDamage(value);

        if(model.Health <= 0)
        {
            //TODO
            Debug.Log("Dead");
        }
    }

    public void DoCombo(ButtonsXbox buttonPressed)
    {
        weaponController.DoCombo(buttonPressed);
    }

    public void StartCharging()
    {
        StartCoroutine(weaponController.StartCharging());
    }

    public void StopCharging()
    {
        weaponController.StopCharging();
    }
}
