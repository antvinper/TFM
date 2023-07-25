using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System.Linq;

public class PlayerController : Characters.CharacterController//<PlayerMutableModel>
{
    private PlayerController instance;
    /**
     * TODO
     * No sé si vale la pena separar enemy y player models
     */
    //[SerializeField] PlayerMutableModel model;
    [SerializeField] List<SkillDefinition> skills = new List<SkillDefinition>();

    public WeaponController weaponController;




    /**
     * TODO
     * Borrar de aquí
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
     * El weapon controller se obtendrá de un menú antes de iniciar el nivel
     * que es donde se elegirá el arma. Por tanto, el PlayerController tiene 2 opciones:
     * 1- Tener todas las armas como hijas y activar la que necesite
     * 2- Instanciar el arma y hacerla hija
     */
    private void Start()
    {
        GameManager.Instance.SetPlayerController(this);
        weaponController = GetComponent<WeaponController>();
        SetModel();

        //TREE BEHAVIOUR
        Debug.Log(model.Tree.Slots);
        for(int i = 0; i < model.Tree.Slots.Count; ++i)
        {
            TreeSlotDefinition slot = model.Tree.Slots[i];
            List<InstantEffectPermanent> effects = model.Tree.Slots[i].Effects;
            Debug.Log("## Slot: " + i + " ## ");
            for(int j = 0; j < effects.Count; ++j)
            {
                Debug.Log("Effect: " + j + " / " + effects[j].name + " / " + effects[j].Value);
            }
            slot.ProcessSlotActivation(this);
        }
        model.Tree.Slots[0].ProcessSlotDeActivation();


        //UseSkill(PlayerEnumSkills.SINGLE_PERCENTAGE_ATTACK, this);
        //UseSkill(PlayerEnumSkills.HEAL_BY_MAX_HEALTH_PERCENTAGE, this);
        //UseSkill(PlayerEnumSkills.SLOW_DOWN, enemy);

        //UseSkills(PlayerEnumSkills.SINGLE_ATTACK, enemy);

        //UseSkill(PlayerEnumSkills.SLOW_DOWN, this);
        //UseSkill(PlayerEnumSkills.PERMANENT, this);
        //UseSkill(PlayerEnumSkills.DEFFENSE_DOWN, enemy);
    }


    private async Task SetModel()
    {
        //await new WaitForSeconds(1.0f);
        GameManager.Instance.GameData.PlayerModel = model;
    }
    public override void ProcessDamage(int value)
    {
        Debug.Log("ProcessDamage in player");
    }

    public override float GetMyRealDamage()
    {
        /**
         * TODO 
         * Calcular correctamente el daño.
         */
        Debug.Log("My real damage = " + model.Attack);
        return model.Attack;
    }

    public async Task UseSkills(PlayerEnumSkills skillName, Characters.CharacterController target)
    {
        foreach(SkillDefinition sd in skills)
        {
            await new WaitForSeconds(2);
            sd.ProcessSkill(this, target);
        }
    }

    public void UseSkill(PlayerEnumSkills skillName, Characters.CharacterController target)
    {
        Debug.Log("Using: " + skillName);
        SkillDefinition skill = skills.Where(s => s.name.Equals(skillName)).FirstOrDefault();
        /*switch (skillName)
        {
            case PlayerEnumSkillsTest.SINGLE_ATTACK:

                break;
        }*/
        
        //skill.ProcessSkill(this);
        skill.ProcessSkill(this, target);
        Debug.Log(model.MaxHealth);
        Debug.Log(model.Health);
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
