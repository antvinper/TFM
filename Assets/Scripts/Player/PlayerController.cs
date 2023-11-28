using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System.Linq;
using CompanyStats;

public class PlayerController : CompanyCharacterController//<PlayerMutableModel>
{
    private PlayerController instance;
    /*private new PlayerMutableModel model;
    public PlayerMutableModel Model
    {
        get => model;
        set => model = value;
    }*/
    
    [SerializeField] List<SkillDefinition> skills = new List<SkillDefinition>();
    private SoulFragment soulFragment;
    private Rupee rupees;

    public WeaponController weaponController;

    [SerializeField] private StatsTree tree;
    //[SerializeField] private StatsDefinition statsDefinitions;
    /**
     * TODO
     * Borrar de aqu�
     */
    //public Characters.CharacterController enemy;

    StatsCanvasSupport statsCanvas;

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
        /**
         * TODO
         * Refactor all the Start Method
         */
        GameManager.Instance.SetPlayerController(this);
        //SetNewModel();

        //Obtener del modelo la cantidad de rupias y fragmentos que tiene
        this.rupees = new Rupee(model.Rupees);
        this.soulFragment = new SoulFragment(model.SoulFragments);



        //TREE BEHAVIOUR
        /*Debug.Log(model.Tree.Slots);
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
        model.Tree.Slots[0].ProcessSlotDeActivation();*/


        //UseSkill(PlayerEnumSkills.SINGLE_PERCENTAGE_ATTACK, this);
        //UseSkill(PlayerEnumSkills.HEAL_BY_MAX_HEALTH_PERCENTAGE, this);
        //UseSkill(PlayerEnumSkills.SLOW_DOWN, enemy);

        //UseSkills(PlayerEnumSkills.HURRY_UP, this);

        //UseSkill(PlayerEnumSkills.SLOW_DOWN, this);
        //UseSkill(PlayerEnumSkills.PERMANENT, this);
        //UseSkill(PlayerEnumSkills.DEFFENSE_DOWN, enemy);
        //GetRoomRewards();

        //ApplySkill();


        statsCanvas = FindObjectOfType<StatsCanvasSupport>();
        WriteStats();
        //ActiveSlotTree(1);
        ApplySkill();
    }

    //TODO to erase
    private void WriteStats()
    {
        /*
        statsCanvas.statsPanelSupport.healthText.text = StatNames.HEALTH + ": " + model.GetStatValue(StatNames.HEALTH, StatParts.ACTUAL_VALUE);
        statsCanvas.statsPanelSupport.manaText.text = StatNames.MANA + ": " + model.GetStatValue(StatNames.MANA, StatParts.ACTUAL_VALUE);
        statsCanvas.statsPanelSupport.attackText.text = StatNames.ATTACK + ": " + model.GetStatValue(StatNames.ATTACK, StatParts.ACTUAL_VALUE);
        statsCanvas.statsPanelSupport.magicalAttackText.text = StatNames.MAGICAL_ATTACK + ": " + model.GetStatValue(StatNames.MAGICAL_ATTACK, StatParts.ACTUAL_VALUE);
        statsCanvas.statsPanelSupport.defenseText.text = StatNames.DEFENSE + ": " + model.GetStatValue(StatNames.DEFENSE, StatParts.ACTUAL_VALUE);
        statsCanvas.statsPanelSupport.magicalDefenseText.text = StatNames.MAGICAL_DEFENSE + ": " + model.GetStatValue(StatNames.MAGICAL_DEFENSE, StatParts.ACTUAL_VALUE);
        statsCanvas.statsPanelSupport.speedText.text = StatNames.SPEED + ": " + model.GetStatValue(StatNames.SPEED, StatParts.ACTUAL_VALUE);
        statsCanvas.statsPanelSupport.evasionText.text = StatNames.EVASION + ": " + model.GetStatValue(StatNames.EVASION, StatParts.ACTUAL_VALUE);
        statsCanvas.statsPanelSupport.critChanceText.text = StatNames.CRIT_CHANCE + ": " + model.GetStatValue(StatNames.CRIT_CHANCE, StatParts.ACTUAL_VALUE);
        statsCanvas.statsPanelSupport.dodgeChanceText.text = StatNames.DODGE_CHANCE + ": " + model.GetStatValue(StatNames.DODGE_CHANCE, StatParts.ACTUAL_VALUE);
        */
    }

    public async Task ActiveSlotTree(int index)
    {
        await new WaitForSeconds(2.0f);
        PlayerMutableModel model = this.model as PlayerMutableModel;
        model.ProcessSlotTreeActivation(index);
        WriteStats();
    }

    public void AddRupees(int value)
    {
        this.rupees.AddAmount(value);
        this.model.Rupees = this.rupees.Amount;
        Debug.Log("#Room# Rupees gained: " + value);
        Debug.Log("#Room# Actual rupees: " + this.model.Rupees);

        //model.Rupees += value;
    }
    public void AddSoulFragments(int value)
    {
        this.soulFragment.AddAmount(value);
        this.model.SoulFragments = this.soulFragment.Amount;
        Debug.Log("#Room# SoulFragments gained: " + value);
        Debug.Log("#Room# Actual SoulFragments: " + this.model.SoulFragments);
    }

    private async Task GetRoomRewards()
    {
        await new WaitForSeconds(2.0f);
        int rupeesGained = RoomManager.Instance.GetRoomRewards();

        AddRupees(rupeesGained);
        

        //Shop
        //Al gameManager
        ShopManager.Instance.CreateShop();

        GetActiveCombos();
    }

    public async Task SetModel(PlayerMutableModel model)
    {
        this.model = model;
        this.model.Setup(characterStatsDefinition);
    }

    public async Task SetNewModel()
    {
        model = new PlayerMutableModel(tree);
        model.Setup(characterStatsDefinition);
        GameManager.Instance.GameModel.PlayerModel = model as PlayerMutableModel;
    }
    public override void ApplyDamage(Strike strike)
    {
        Debug.Log("Health before damage = " + model.GetStatValue(StatNames.HEALTH, StatParts.ACTUAL_VALUE));
        bool isAlive = model.ApplyDamage(strike);
        Debug.Log("Applied an attack of: " + strike.FinalValue + " points");
        Debug.Log("Health after damage = " + model.GetStatValue(StatNames.HEALTH, StatParts.ACTUAL_VALUE));

        if (!isAlive)
        {
            Debug.Log("TODO -> Behaviour when dies. Maybe should override method if enemy or player");
        }
        WriteStats();
    }

    /*public override float GetMyRealDamage()
    {
        Debug.Log("My real damage = " + model.GetStatValue(StatsEnum.ATTACK));
        return model.GetStatValue(StatsEnum.ATTACK);
    }*/

    public async Task ApplySkill()
    {
        await new WaitForSeconds(2.0f);
        foreach (SkillDefinition skill in skills)
        {
            skill.ProcessSkill(this, this);
            await new WaitForSeconds(3.0f);
        }
        foreach (SkillDefinition skill in skills)
        {
            skill.ProcessSkill(this);
            await new WaitForSeconds(3.0f);
        }
    }

    public async Task UseSkills(List<SkillDefinition> skillName, CompanyCharacterController target)
    {
        foreach(SkillDefinition sd in skills)
        {
            await new WaitForSeconds(2);
            //Instant, During, Over
            //sd.ProcessSkill(this, target);
            //Permanent
            //sd.ProcessSkill(this);
        }

        
    }

    public async Task UseSkill(SkillDefinition skillName, CompanyCharacterController target)
    {
        Debug.Log("Using: " + skillName);
        SkillDefinition skill = skills.Where(s => s.name.Equals(skillName)).FirstOrDefault();
        await new WaitForSeconds(6);
        /*switch (skillName)
        {
            case PlayerEnumSkillsTest.SINGLE_ATTACK:

                break;
        }*/

        //skill.ProcessSkill(this);
        //skill.ProcessSkill(this, target);
        //Debug.Log(model.MaxHealth);
        //Debug.Log(model.GetStatValue(StatsEnum.HEALTH));
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
        weaponController.StartCharging();
        //StartCoroutine(weaponController.StartCharging());
    }

    public void StopCharging()
    {
        weaponController.StopCharging();
    }

    public List<BasicComboDefinition> GetActiveCombos()
    {
        List<BasicComboDefinition> activeCombos = weaponController.GetActiveCombos();
        foreach(BasicComboDefinition comb in activeCombos)
        {
            Debug.Log("Active Combo: " + comb.Name);
        }
        return activeCombos;
    }

    public List<BasicComboDefinition> GetInactiveCombos()
    {
        List<BasicComboDefinition> inactiveCombos = weaponController.GetInactiveCombos();
        foreach (BasicComboDefinition comb in inactiveCombos)
        {
            Debug.Log("Inactive Combo: " + comb.Name);
        }
        return inactiveCombos;
    }


    #region AnimEvents
    public void CanContinueComboAnimEvent()
    {
        weaponController.CanContinueCombo();
    }
    public void CanNotContinueComboAnimEvent()
    {
        weaponController.CanNotContinueCombo();
    }
    public void CancelComboAnimEvent()
    {
        weaponController.CancelComboFromAnim();
    }
    public void CanMakeDamageAnimEvent()
    {
        weaponController.CanMakeDamage();
    }
    public void CanNotMakeDamageAnimEvent()
    {
        weaponController.CanNotMakeDamage();
    }
    public void FinishComboAnimEvent()
    {
        weaponController.FinishCombo();
    }
    #endregion
}
