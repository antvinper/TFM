using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System.Linq;
using CompanyStats;
using System;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerController : CompanyCharacterController//<PlayerMutableModel>
{
    [Header("Transforms")]
    [SerializeField] Transform parentTransform;
    /*[Header("Transforms")]
    [SerializeField] Transform parentTransform;
    [SerializeField] Transform cameraTransform;
    [SerializeField] Transform virtualCameraTransform;*/

    [Header("Transform offsets")]
    [SerializeField] private Vector3 selfPositionOffset;
    [SerializeField] private Quaternion selfRotationOffset;

    Transform selfOffset = null;
    Transform cameraOffset = null;
    Transform virtualCameraOffset = null;

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


    [Space(10)]
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

    [SerializeField] private Animator animator;

    [Header("Scripts and objects that must be de/activated")]
    [SerializeField] GameObject camera;


    [SerializeField] GameObject virtualCamera;
    [SerializeField] MovePlayer movePlayer;
    [SerializeField] PlayerInputManager playerInputManager;
    [SerializeField] CapsuleCollider capsuleCollider;
    [SerializeField] PlayerInput playerInput;

    public MovePlayer MovePlayer { get => movePlayer; } 


    internal void CameraSetActive(bool isActive)
    {
        camera.SetActive(isActive);
        virtualCamera.SetActive(isActive);
    }

    internal async Task ActivateControls()
    {
        camera.SetActive(true);
        virtualCamera.SetActive(true);
        virtualCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.NearClipPlane = -10;
        movePlayer.ContinueMovement();
        movePlayer.CharacterController.enabled = true;

        await new WaitForSeconds(0.3f);
        //await new WaitUntil(() => !movePlayer.CharacterController.isGrounded);
        await new WaitUntil(() => movePlayer.CharacterController.isGrounded);

        movePlayer.SetInputControls(true);
        playerInput.enabled = true;
        playerInputManager.enabled = true;

        movePlayer.ActivateControls();
    }

    internal void DeActivateControls(bool deActivateCamera)
    {
        playerInputManager.enabled = false;
        playerInput.enabled = false;

        movePlayer.StopMovement();
        movePlayer.DeActivateControls();

        //capsuleCollider.enabled = false;

        if (deActivateCamera)
        {
            virtualCamera.SetActive(false);
            camera.SetActive(false);
        }
    }

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
        this.rupees = new Rupee(playerModel.Rupees);
        this.soulFragments = new SoulFragment(playerModel.SoulFragments);
        StatsTree t = new StatsTree(tree);
        this.playerModel.Tree = t;
        this.playerModel.Setup(characterStatsDefinition);
        base.Setup(playerModel);
    }

    public async Task SetUp()
    {
        StatsTree t = new StatsTree(tree);
        playerModel = new PlayerMutableModel(t);
        this.rupees = new Rupee(playerModel.Rupees);
        this.soulFragments = new SoulFragment(playerModel.SoulFragments);
        playerModel.Setup(characterStatsDefinition);
        GameManager.Instance.GameModel.PlayerModel = playerModel as PlayerMutableModel;
        base.Setup(playerModel);

    }
    private void SetOffsets()
    {
        if(cameraOffset == null)
        {
            //cameraOffset = camera.transform;
        }
        if(virtualCameraOffset == null)
        {
            //virtualCameraOffset = virtualCamera.transform;
        }
        if(selfOffset == null)
        {
            //selfOffset = transform;
            selfPositionOffset = transform.position;
            selfRotationOffset = transform.rotation;
        }
    }

    internal void SetStartPosition(Transform playerSpawnPoint)
    {
        parentTransform.position = playerSpawnPoint.position;
        parentTransform.rotation = playerSpawnPoint.rotation;

        /*camera.transform.position = cameraOffset.position;
        camera.transform.rotation = cameraOffset.rotation;

        virtualCamera.transform.position = virtualCameraOffset.position;
        virtualCamera.transform.rotation = virtualCameraOffset.rotation;*/

        //transform.position = selfOffset.position;
        //transform.position = selfPositionOffset;
        //transform.rotation = selfOffset.rotation;
        //transform.rotation = selfRotationOffset;

        //Debug.Log("#PLAYER player set in position");
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
        if(cameraOffset == null || selfOffset == null || virtualCameraOffset == null)
        {
            SetOffsets();
        }

        /**
         * TODO
         * Refactor all the Start Method
         */
        GameManager.Instance.SetPlayerController(this);

        /*this.rupees = new Rupee(playerModel.Rupees);
        this.soulFragments = new SoulFragment(playerModel.SoulFragments);*/

        statsCanvas = FindObjectOfType<StatsCanvasSupport>();
        //ActiveSlotTree(1);
        //ApplySkill();

        //TODO -> Erase from here. Just for testing
        //AddSoulFragments(100);
    }



    public async Task IncrementSlotTree(TreeSlot slot)
    {
        playerModel.ProcessIncrementSlotTree(slot);
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
        //Debug.Log("Health before damage = " + playerModel.GetStatValue(StatNames.HEALTH, StatParts.ACTUAL_VALUE));
        bool isAlive = playerModel.ApplyDamage(strike);
        //Debug.Log("Applied an attack of: " + strike.FinalValue + " points");
        //Debug.Log("Health after damage = " + playerModel.GetStatValue(StatNames.HEALTH, StatParts.ACTUAL_VALUE));

        if (!isAlive)
        {
            Debug.Log("TODO -> Behaviour when dies. Maybe should override method if enemy or player");
        }
    }

    /*public async Task ApplySkill()
    {
        await new WaitForSeconds(2.0f);
        foreach (SkillDefinition skill in skills)
        {
            skill.ProcessSkill(this, this);
            await new WaitForSeconds(2.0f);
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

    public void SetDashing(bool isDashing)
    {
        animator.SetBool("isDashing", isDashing);
    }

    public void DoDash(float dashingTime = 0.35f, float dashingPower = 1.0f)
    {
        GetComponent<MovePlayer>().DoDash(dashingTime, dashingPower);
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

    public void ContinueMovement()
    {
        Debug.Log("#MOVE -> MOVE");
        GetComponent<MovePlayer>().ContinueMovement();
    }
    public void StopMovement()
    {
        Debug.Log("#MOVE -> STOP");
        GetComponent<MovePlayer>().StopMovement();
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
        Debug.Log("#COMBO CancelComboAnimEvent");

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
