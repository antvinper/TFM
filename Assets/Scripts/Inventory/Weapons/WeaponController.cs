using CompanyStats;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class WeaponController: MonoBehaviour
{
    protected float actualTimeCharging = 0;
    private bool isPreCharging = false;
    protected bool isCharging = false;
    protected int actualIndex = 0;
    private int comboIndex = -1;
    protected bool doingCombo = false;
    protected bool canCancelCombo = false;
    protected bool comboStarted;
    protected bool canAttack;
    private bool canMakeDamage = false;
    private bool isComboFinished = false;
    private bool isComboFinishing = false;
    private bool isComboCanceled = false;

    private bool canContinueCombo = false;


    private List<ButtonsXbox> actualActionStack;
    [SerializeField] protected SkillDefinition chargedSkill;

    public int ActualIndex { get => actualIndex; set => actualIndex = value; }
    public bool DoingCombo { get => doingCombo; set => doingCombo = value; }
    public bool CanCancelCombo { get => canCancelCombo; set => canCancelCombo = value; }

    [SerializeField] private Animator animator;

    private float timeBetweenCombos = 0.5f;
    private bool isTimeBetweenCombosCompleted= true;

    WeaponModel model;

    internal void SetIsDoingCombo(bool isDoingCombo)
    {
        this.doingCombo = isDoingCombo;
    }

    public void Setup(WeaponModel model)
    {
        this.model = model;

        canAttack = true;
        int maxComboLength = 0;
        foreach (BasicComboDefinition basicComboDefinition in this.model.BasicComboDefinitions)
        {
            basicComboDefinition.SetUp();
            int comboLength = basicComboDefinition.GetComboLength();
            maxComboLength = comboLength > maxComboLength ? comboLength : maxComboLength;
        }
        actualActionStack = new List<ButtonsXbox>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Enemy") && canMakeDamage)
        {
            CanNotMakeDamage();
            PlayerController owner = GetComponentInParent<PlayerController>();
            EnemyController target = other.GetComponentInParent<EnemyController>();
            if(owner != null && target != null)
            {
                model.BasicComboDefinitions[comboIndex++].UseSkill(owner, target, this);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Enemy") && canMakeDamage)
        {
            CanNotMakeDamage();
            PlayerController owner = GetComponentInParent<PlayerController>();
            EnemyController target = other.GetComponentInParent<EnemyController>();
            if (owner != null && target != null)
            {
                model.BasicComboDefinitions[comboIndex++].UseSkill(owner, target, this);
            }
        }
    }

    /*
     * 1- Aprieto bot�n bueno: Se inicia corrutina de cancelaci�n
     *      2A- Aprieto bot�n continuaci�n combo: Se cancela corrutina
     *      2B- Aprieto bot�n malo: Se cancela corrutina y se cancela el combo
     *      2C- No aprieto nada durante el tiempo de la corrutina: Se cancela el combo.
     *      
     */

    public void CanContinueCombo()
    {
        canContinueCombo = true;
    }
    public void CanNotContinueCombo()
    {
        canContinueCombo = false;
    }

    

    private void ResetCombos()
    {
        foreach (BasicComboDefinition combo in model.BasicComboDefinitions)
        {
            combo.Reset();
        }
    }
    public void CancelComboFromAnim()
    {
        if (doingCombo || actualIndex == 0)
        {
            canContinueCombo = false;
            doingCombo = false;
            CancelCombo();
        }
    }
    public async Task CancelCombo()
    {
        if (!isComboCanceled)
        {
            isComboCanceled = true;
            GameManager.Instance.GetPlayerController().ContinueMovement();
            if (!doingCombo && !isComboFinished)
            {
                animator.SetTrigger("cancelCombo");
                comboStarted = false;
                actualIndex = 0;
                actualActionStack.Clear();
                ResetCombos();
                await StartTimerBetweenCombosCompleted();
                isComboCanceled = false;
            }
        }
    }

    private async Task StartTimerBetweenCombosCompleted()
    {
        isTimeBetweenCombosCompleted = false;
        await new WaitForSeconds(timeBetweenCombos);
        isTimeBetweenCombosCompleted = true;
    }

    public async Task FinishCombo()
    {
        isComboFinished = true;
        doingCombo = false;
        canAttack = false;

        actualActionStack.Clear();
        actualIndex = 0;
        canAttack = true;
        isComboFinished = false;
        ResetCombos();
        comboStarted = false;
        isComboFinishing = false;
        GameManager.Instance.GetPlayerController().ContinueMovement();
        await StartTimerBetweenCombosCompleted();
    }

    public void DoCombo(ButtonsXbox buttonPressed)
    {
        if (canAttack && !isCharging && !isComboFinishing && isTimeBetweenCombosCompleted)
        {
            if (!comboStarted)
            {
                StartCombo(buttonPressed);
            }
            else
            {
                ContinueCombo(buttonPressed);
            }
        }
        
    }
    protected void StartCombo(ButtonsXbox buttonPressed)
    {
        if (isTimeBetweenCombosCompleted)
        {
            for (int i = 0; i < model.BasicComboDefinitions.Length; ++i)
            {
                if (model.BasicComboDefinitions[i].StartCombo(buttonPressed))
                {
                    GameManager.Instance.GetPlayerController().StopMovement();
                    comboIndex = i;
                    comboStarted = true;
                    actualActionStack.Add(buttonPressed);
                    animator.SetTrigger(model.BasicComboDefinitions[i].AnimationTriggerToStart.ToString());
                    break;
                }
            }
        }
        
    }

    public void ContinueAnimationCombo()
    {
        animator.SetTrigger("continueCombo");
    }

    private void ContinueCombo(ButtonsXbox buttonPressed)
    {
        if (canContinueCombo)
        {
            bool isComboContinued = false;
            NextComboStep(buttonPressed);

            for (int i = 0; i < model.BasicComboDefinitions.Length; ++i)
            {
                isComboContinued = model.BasicComboDefinitions[i].ContinueCombo(buttonPressed, actualActionStack, this);
                if (model.BasicComboDefinitions[i].IsLastComboIndex())
                {
                    isComboFinishing = true;
                }
                if (isComboContinued)
                {
                    comboIndex = i;
                    isComboContinued = true;
                    ContinueAnimationCombo();
                    canContinueCombo = false;
                    break;
                }
            }

            if (!isComboContinued && !isComboFinishing)
            {
                CancelCombo();
            }
        }
        
    }

    private void NextComboStep(ButtonsXbox buttonPressed)
    {
        actualActionStack.Add(buttonPressed);
        ++actualIndex;
    }
    

    public void StopCharging()
    {
        isPreCharging = false;
        isCharging = false;
        actualTimeCharging = 0;
        //TODO Ataque cargado.
        //chargedSkill.ProcessSkill(owner, target);
    }

    public async Task StartCharging()
    {
        if (!canContinueCombo)
        {
            isPreCharging = true;
            await new WaitForSeconds(0.2f);
            if (isPreCharging && this.actualIndex == 1)
            {
                isCharging = true;
                while (isCharging && actualTimeCharging < model.MaxTimeCharge)
                {
                    actualTimeCharging += Time.deltaTime;
                    Debug.Log("Time recharging = " + actualTimeCharging);
                    await new WaitForSeconds(Time.deltaTime);
                }
            }
        }
    }

    public void CanMakeDamage()
    {
        canMakeDamage = true;
    }
    public void CanNotMakeDamage()
    {
        canMakeDamage = false;
    }

    public List<BasicComboDefinition> GetActiveCombos()
    {
        List<BasicComboDefinition> activeCombos = new List<BasicComboDefinition>();
        activeCombos = model.BasicComboDefinitions.ToList();
        return activeCombos.Where(t => t.IsActive).ToList();
    }

    internal List<BasicComboDefinition> GetInactiveCombos()
    {
        List<BasicComboDefinition> activeCombos = new List<BasicComboDefinition>();
        activeCombos = model.BasicComboDefinitions.ToList();
        return activeCombos.Where(t => !t.IsActive).ToList();
    }

}
