using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System.Linq;

public class PlayerController : Characters.CharacterController//<PlayerMutableModel>
{
    private PlayerController instance;
    /**
     * TODO
     * No s� si vale la pena separar enemy y player models
     */
    //[SerializeField] PlayerMutableModel model;
    [SerializeField] List<SkillDefinition> skills = new List<SkillDefinition>();

    public WeaponController weaponController;




    /**
     * TODO
     * Borrar de aqu�
     */
    public Characters.CharacterController enemy;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    /**
     * TODO
     * borrar este start y ponerlo donde toque
     * El weapon controller se obtendr� de un men� antes de iniciar el nivel
     * que es donde se elegir� el arma. Por tanto, el PlayerController tiene 2 opciones:
     * 1- Tener todas las armas como hijas y activar la que necesite
     * 2- Instanciar el arma y hacerla hija
     */
    private void Start()
    {
        GameManager.Instance.SetPlayerController(this);
        weaponController = GetComponent<WeaponController>();
        SetModel();
        UseSkill(PlayerEnumSkillsTest.SINGLE_ATTACK, enemy);
        UseSkill(PlayerEnumSkillsTest.SLOW_DOWN, enemy);
    }

    private async Task SetModel()
    {
        await new WaitForSeconds(1.0f);
        GameManager.Instance.GameData.PlayerModel = model;
    }
    public override void ProcessDamage(float value)
    {
        Debug.Log("ProcessDamage in player");
    }

    public override float GetMyRealDamage()
    {
        /**
         * TODO 
         * Calcular correctamente el da�o.
         */
        Debug.Log("My real damage = " + model.Attack);
        return model.Attack;
    }

    public void UseSkill(PlayerEnumSkillsTest skillName, Characters.CharacterController target)
    {
        Debug.Log("Using: " + skillName);
        SkillDefinition skill = skills.Where(s => s.name.Equals(skillName)).FirstOrDefault();
        /*switch (skillName)
        {
            case PlayerEnumSkillsTest.SINGLE_ATTACK:

                break;
        }*/
        
        skill.ProcessSkill(this, target);
    }

    /*public void ProcessDamage(float value)
    {
        model.TakeDamage(value);

        if(model.Health <= 0)
        {
            //TODO
            Debug.Log("Dead");
        }
    }*/

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
