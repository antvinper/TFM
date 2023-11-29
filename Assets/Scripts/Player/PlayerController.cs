using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System.Linq;
using CompanyStats;

public class PlayerController : CompanyCharacterController//<PlayerMutableModel>
{
    private PlayerController instance;

    protected new PlayerMutableModel model;
    new public PlayerMutableModel Model
    {
        get => this.model;
        set => this.model = value;
    }

    [SerializeField] List<SkillDefinition> skills = new List<SkillDefinition>();
    private SoulFragment soulFragments;
    private Rupee rupees;

    public SoulFragment SoulFragments
    {
        get => soulFragments;
    }
    public Rupee Rupees
    {
        get => rupees;
    }

    //TODO -> Make it private and get it from WeaponActive
    public WeaponController weaponController;

    [SerializeField] private StatsTreeDefinition tree;

    /**
     * TODO
     * Borrar de aquí y obtenerlo con colisiones
     */
    //public Characters.CharacterController enemy;

    public CanvasTreeManager canvasTreeManager;
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

    public async Task SetModel(PlayerMutableModel model)
    {
        this.model = model;
        StatsTree t = new StatsTree(tree);
        this.model.Tree = t;
        this.model.Setup(characterStatsDefinition);
    }

    public async Task SetNewModel()
    {
        StatsTree t = new StatsTree(tree);
        model = new PlayerMutableModel(t);
        model.Setup(characterStatsDefinition);
        GameManager.Instance.GameModel.PlayerModel = model as PlayerMutableModel;

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

        this.rupees = new Rupee(model.Rupees);
        this.soulFragments = new SoulFragment(model.SoulFragments);

        //GetRoomRewards();




        statsCanvas = FindObjectOfType<StatsCanvasSupport>();
        WriteStats();
        //ActiveSlotTree(1);
        ApplySkill();
        canvasTreeManager.Setup(model.Tree.Slots);

        //TODO -> Erase from here. Just for testing
        AddSoulFragments(100);
    }

    //TODO to erase
    private void WriteStats()
    {
        statsCanvas.statsPanelSupport.healthText.text = StatNames.HEALTH + ": " + model.GetStatValue(StatNames.HEALTH, StatParts.ACTUAL_VALUE);
        statsCanvas.statsPanelSupport.actualMaxHealthText.text = StatNames.HEALTH + " ACTUAL MAX: " + model.GetStatValue(StatNames.HEALTH, StatParts.ACTUAL_MAX_VALUE);
        statsCanvas.statsPanelSupport.manaText.text = StatNames.MANA + ": " + model.GetStatValue(StatNames.MANA, StatParts.ACTUAL_VALUE);
        statsCanvas.statsPanelSupport.attackText.text = StatNames.ATTACK + ": " + model.GetStatValue(StatNames.ATTACK, StatParts.ACTUAL_VALUE);
        statsCanvas.statsPanelSupport.magicalAttackText.text = StatNames.MAGICAL_ATTACK + ": " + model.GetStatValue(StatNames.MAGICAL_ATTACK, StatParts.ACTUAL_VALUE);
        statsCanvas.statsPanelSupport.defenseText.text = StatNames.DEFENSE + ": " + model.GetStatValue(StatNames.DEFENSE, StatParts.ACTUAL_VALUE);
        statsCanvas.statsPanelSupport.magicalDefenseText.text = StatNames.MAGICAL_DEFENSE + ": " + model.GetStatValue(StatNames.MAGICAL_DEFENSE, StatParts.ACTUAL_VALUE);
        statsCanvas.statsPanelSupport.speedText.text = StatNames.SPEED + ": " + model.GetStatValue(StatNames.SPEED, StatParts.ACTUAL_VALUE);
        statsCanvas.statsPanelSupport.evasionText.text = StatNames.EVASION + ": " + model.GetStatValue(StatNames.EVASION, StatParts.ACTUAL_VALUE);
        statsCanvas.statsPanelSupport.critChanceText.text = StatNames.CRIT_CHANCE + ": " + model.GetStatValue(StatNames.CRIT_CHANCE, StatParts.ACTUAL_VALUE);
        statsCanvas.statsPanelSupport.dodgeChanceText.text = StatNames.DODGE_CHANCE + ": " + model.GetStatValue(StatNames.DODGE_CHANCE, StatParts.ACTUAL_VALUE);
    }


    public async Task IncrementSlotTree(TreeSlot slot)
    {
        model.ProcessIncrementSlotTree(slot);
        WriteStats();
    }


    public void AddRupees(int value)
    {
        this.rupees.AddAmount(value);
        this.model.Rupees = this.rupees.Amount;
        Debug.Log("#Room# Rupees gained: " + value);
        Debug.Log("#Room# Actual rupees: " + this.model.Rupees);
    }
    public void AddSoulFragments(int value)
    {
        if(value > 0)
        {
            this.soulFragments.AddAmount(value);
        }
        else
        {
            this.soulFragments.RemoveAmount(-value);
        }
        
        this.model.SoulFragments = this.soulFragments.Amount;
        Debug.Log("#Room# SoulFragments gained: " + value);
        Debug.Log("#Room# Actual SoulFragments: " + this.model.SoulFragments);
    }

    //TODO -> Esto debería ir en algún RoomManager
    private async Task GetRoomRewards()
    {
        await new WaitForSeconds(2.0f);
        int rupeesGained = RoomManager.Instance.GetRoomRewards();

        AddRupees(rupeesGained);
        

        //TODO -> Shop a GameManager
        ShopManager.Instance.CreateShop();

        GetActiveCombos();
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

    public void DoCombo(ButtonsXbox buttonPressed)
    {
        weaponController.DoCombo(buttonPressed);
    }

    public void StartCharging()
    {
        weaponController.StartCharging();
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
