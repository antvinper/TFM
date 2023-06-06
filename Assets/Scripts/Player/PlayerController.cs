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
     * El weapon controller se obtendrá de un menú antes de iniciar el nivel
     * que es donde se elegirá el arma. Por tanto, el PlayerController tiene 2 opciones:
     * 1- Tener todas las armas como hijas y activar la que necesite
     * 2- Instanciar el arma y hacerla hija
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
