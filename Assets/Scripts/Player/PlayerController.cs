using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System.Linq;
using CompanyStats;

public class PlayerController : CompanyCharacterController//<PlayerMutableModel>
{
    private PlayerController instance;

    private PlayerMutableModel playerModel;
    public PlayerMutableModel PlayerModel
    {
        get => playerModel;
        set => playerModel = value;
    }

    //[SerializeField] List<SkillDefinition> skills = new List<SkillDefinition>();
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

    //public EnemyController enemy;

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

    public async Task SetUp(PlayerMutableModel model)
    {
        this.playerModel = model;
        StatsTree t = new StatsTree(tree);
        this.playerModel.Tree = t;
        this.playerModel.Setup(characterStatsDefinition);
        base.Setup(playerModel);
    }

    public async Task SetUp()
    {
        StatsTree t = new StatsTree(tree);
        playerModel = new PlayerMutableModel(t);
        playerModel.Setup(characterStatsDefinition);
        GameManager.Instance.GameModel.PlayerModel = playerModel as PlayerMutableModel;
        base.Setup(playerModel);

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

        this.rupees = new Rupee(playerModel.Rupees);
        this.soulFragments = new SoulFragment(playerModel.SoulFragments);

        




        statsCanvas = FindObjectOfType<StatsCanvasSupport>();
        WriteStats();
        //ActiveSlotTree(1);
        //ApplySkill();
        //canvasTreeManager.Setup(playerModel.Tree.Slots);

        //TODO -> Erase from here. Just for testing
        AddSoulFragments(100);
    }

    //TODO to erase
    private void WriteStats()
    {
        
        /*statsCanvas.statsPanelSupport.healthText.text = StatNames.HEALTH + ": " + playerModel.GetStatValue(StatNames.HEALTH, StatParts.ACTUAL_VALUE);
        statsCanvas.statsPanelSupport.actualMaxHealthText.text = StatNames.HEALTH + " ACTUAL MAX: " + playerModel.GetStatValue(StatNames.HEALTH, StatParts.ACTUAL_MAX_VALUE);
        statsCanvas.statsPanelSupport.manaText.text = StatNames.MANA + ": " + playerModel.GetStatValue(StatNames.MANA, StatParts.ACTUAL_VALUE);
        statsCanvas.statsPanelSupport.attackText.text = StatNames.ATTACK + ": " + playerModel.GetStatValue(StatNames.ATTACK, StatParts.ACTUAL_VALUE);
        statsCanvas.statsPanelSupport.magicalAttackText.text = StatNames.MAGICAL_ATTACK + ": " + playerModel.GetStatValue(StatNames.MAGICAL_ATTACK, StatParts.ACTUAL_VALUE);
        statsCanvas.statsPanelSupport.defenseText.text = StatNames.DEFENSE + ": " + playerModel.GetStatValue(StatNames.DEFENSE, StatParts.ACTUAL_VALUE);
        statsCanvas.statsPanelSupport.magicalDefenseText.text = StatNames.MAGICAL_DEFENSE + ": " + playerModel.GetStatValue(StatNames.MAGICAL_DEFENSE, StatParts.ACTUAL_VALUE);
        statsCanvas.statsPanelSupport.speedText.text = StatNames.SPEED + ": " + playerModel.GetStatValue(StatNames.SPEED, StatParts.ACTUAL_VALUE);
        statsCanvas.statsPanelSupport.evasionText.text = StatNames.EVASION + ": " + playerModel.GetStatValue(StatNames.EVASION, StatParts.ACTUAL_VALUE);
        statsCanvas.statsPanelSupport.critChanceText.text = StatNames.CRIT_CHANCE + ": " + playerModel.GetStatValue(StatNames.CRIT_CHANCE, StatParts.ACTUAL_VALUE);
        statsCanvas.statsPanelSupport.dodgeChanceText.text = StatNames.DODGE_CHANCE + ": " + playerModel.GetStatValue(StatNames.DODGE_CHANCE, StatParts.ACTUAL_VALUE);
        statsCanvas.statsPanelSupport.luckyText.text = StatNames.LUCKY + ": " + playerModel.GetStatValue(StatNames.LUCKY, StatParts.ACTUAL_VALUE);*/
        
    }


    public async Task IncrementSlotTree(TreeSlot slot)
    {
        playerModel.ProcessIncrementSlotTree(slot);
        WriteStats();
    }


    public void AddRupees(int value)
    {
        this.rupees.AddAmount(value);
        this.playerModel.Rupees = this.rupees.Amount;
        Debug.Log("#Room# Rupees gained: " + value);
        Debug.Log("#Room# Actual rupees: " + this.playerModel.Rupees);
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
        
        this.playerModel.SoulFragments = this.soulFragments.Amount;
        Debug.Log("#Room# SoulFragments gained: " + value);
        Debug.Log("#Room# Actual SoulFragments: " + this.playerModel.SoulFragments);
    }

    //TODO -> Esto debería de crearse de otra manera -> al tomar una acción
    private async Task CreateShop()
    {
        //ShopManager.Instance.CreateShop();

        //GetActiveCombos();
    }

    
    public override void ApplyDamage(Strike strike)
    {
        Debug.Log("Health before damage = " + playerModel.GetStatValue(StatNames.HEALTH, StatParts.ACTUAL_VALUE));
        bool isAlive = playerModel.ApplyDamage(strike);
        Debug.Log("Applied an attack of: " + strike.FinalValue + " points");
        Debug.Log("Health after damage = " + playerModel.GetStatValue(StatNames.HEALTH, StatParts.ACTUAL_VALUE));

        if (!isAlive)
        {
            Debug.Log("TODO -> Behaviour when dies. Maybe should override method if enemy or player");
        }
        WriteStats();
    }

    /*public async Task ApplySkill()
    {
        await new WaitForSeconds(2.0f);
        foreach (SkillDefinition skill in skills)
        {
            skill.ProcessSkill(this, enemy);
            await new WaitForSeconds(3.0f);
        }
        foreach (SkillDefinition skill in skills)
        {
            skill.ProcessSkill(this);
            await new WaitForSeconds(3.0f);
        }
    }*/

    /*public override Stat GetStatFromName(StatNames statName)
    {
        return model.GetStatFromName(statName);
    }

    public override int GetStatValue(StatNames statName, StatParts statPart)
    {
        if (model == null)
        {
            Debug.Log("NULL");
        }
        return model.GetStatValue(statName, statPart);
    }*/

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
