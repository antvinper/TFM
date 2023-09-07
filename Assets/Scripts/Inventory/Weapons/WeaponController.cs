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

    private bool canContinueCombo = false;

    //private CancellationTokenSource tokenCancelComboAfterTime;

    private List<ButtonsXbox> actualActionStack;

    public int ActualIndex { get => actualIndex; set => actualIndex = value; }
    public bool DoingCombo { get => doingCombo; set => doingCombo = value; }
    public bool CanCancelCombo { get => canCancelCombo; set => canCancelCombo = value; }

    [SerializeField] private Animator animator;

    //TODO
    //Quizás sea necesario crear una lista de estas corrutinas.
    //Coroutine cancelComboAfterTime;

    WeaponModel model;

    private void Start()
    {
        
    }

    public void Setup(WeaponModel model)
    {
        this.model = model;

        canAttack = true;
        int maxComboLength = 0;
        foreach (BasicComboDefinition basicComboDefinition in this.model.BasicComboDefinitions)
        {
            basicComboDefinition.SetUp(this);
            int comboLength = basicComboDefinition.GetComboLength();
            maxComboLength = comboLength > maxComboLength ? comboLength : maxComboLength;
        }
        actualActionStack = new List<ButtonsXbox>();
        //actualActionStack = new ButtonsXbox[maxComboLength];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Enemy") && canMakeDamage)
        {
            Debug.Log(actualIndex);
            if (actualIndex == 2)
            {
                Debug.Log("H");
            }
            PlayerController owner = GetComponentInParent<PlayerController>();
            EnemyController target = other.GetComponentInParent<EnemyController>();
            if(owner != null && target != null)
            {
                model.BasicComboDefinitions[comboIndex++].UseSkill(owner, other.gameObject.GetComponent<EnemyController>());
            }
            
        }
    }

    /*
     * 1- Aprieto botón bueno: Se inicia corrutina de cancelación
     *      2A- Aprieto botón continuación combo: Se cancela corrutina
     *      2B- Aprieto botón malo: Se cancela corrutina y se cancela el combo
     *      2C- No aprieto nada durante el tiempo de la corrutina: Se cancela el combo.
     *      
     */

    public void CanContinueComboAnimEvent()
    {
        canContinueCombo = true;
    }
    public void CanNotContinueComboAnimEvent()
    {
        canContinueCombo = false;
    }

    public void CancelEventAnimCombo()
    {
        Debug.Log(doingCombo + " " + actualIndex);
        if (!doingCombo || actualIndex == 0)
        {
            canContinueCombo = false;
            doingCombo = false;
            CancelCombo();
        }
        
    }

    private void ResetCombos()
    {
        foreach (BasicComboDefinition combo in model.BasicComboDefinitions)
        {
            combo.ResetActualIndex();
        }
    }

    public void CancelCombo()
    {
        if (!doingCombo && !isComboFinished)
        {
            animator.SetTrigger("cancelCombo");
            doingCombo = false;
            comboStarted = false;
            actualIndex = 0;
            actualActionStack.Clear();
            ResetCombos();
        }
        
    }

    public async Task FinishCombo()
    {
        //tokenCancelComboAfterTime.Cancel();
        isComboFinished = true;
        doingCombo = false;
        comboStarted = false;
        canAttack = false;
        animator.SetTrigger("finishCombo");
        await new WaitForSeconds(0.2f);
        actualActionStack.Clear();
        actualIndex = 0;
        canAttack = true;
        isComboFinished = false;
        ResetCombos();

    }

    
    //TODO
    //Cambiar esta función por un animationEvent que invoque el ResetCombo cuando corresponda.
    /*async Task CancelComboAfterTime()
    {
        tokenCancelComboAfterTime = new CancellationTokenSource();
        Debug.Log("#COMBO# Starting Cancel Coroutine");
        float timeBetweenAtacks = 0.0f;

        while (timeBetweenAtacks <= 1f)
        {
            int t = (int)(Time.deltaTime * 1000);
            await Task.Delay(t, tokenCancelComboAfterTime.Token);
            timeBetweenAtacks += Time.deltaTime;
        }

        if (timeBetweenAtacks >= 1f)
        {
            CancelCombo();
        }

        Debug.Log("#COMBO# Coroutine ended");
    }*/

    public void DoCombo(ButtonsXbox buttonPressed)
    {
        //Debug.Log("Button pressed: " + buttonPressed);
        if (canAttack && !isCharging)
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
        for (int i = 0; i < model.BasicComboDefinitions.Length; ++i)
        {
            if (model.BasicComboDefinitions[i].StartCombo(buttonPressed))
            {
                comboIndex = i;
                comboStarted = true;
                //CancelComboAfterTime();
                actualActionStack.Add(buttonPressed);
                animator.SetTrigger(model.BasicComboDefinitions[i].AnimationTriggerToStart);
                break;
            }
        }

    }

    public void ContinueAnimationCombo()
    {
        animator.SetTrigger("continueCombo");
    }

    private void ContinueCombo(ButtonsXbox buttonPressed)
    {
        //TODO
        if (canContinueCombo)
        {
            bool isComboContinued = false;
            for (int i = 0; i < model.BasicComboDefinitions.Length; ++i)
            {
                isComboContinued = model.BasicComboDefinitions[i].ContinueCombo(buttonPressed, actualActionStack);
                if (isComboContinued)
                {
                    comboIndex = i;
                    //cancelamos la corrutina y empezamos otra
                    //tokenCancelComboAfterTime.Cancel();
                    //CancelComboAfterTime();
                    NextComboStep(buttonPressed);
                    isComboContinued = true;
                    ContinueAnimationCombo();
                    canContinueCombo = false;
                    break;
                }
            }

            if (!isComboContinued)
            {
                CancelCombo();
                //tokenCancelComboAfterTime.Cancel();
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
    }

    public async Task StartCharging()
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
