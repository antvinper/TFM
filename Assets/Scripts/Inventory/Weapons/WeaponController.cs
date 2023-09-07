using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class WeaponController: MonoBehaviour
{
    protected float actualTimeCharging = 0;
    protected bool isCharging = false;
    protected int actualIndex = 0;
    protected bool doingCombo = false;
    protected bool canCancelCombo = false;
    protected bool comboStarted;
    protected bool canAttack;

    private List<ButtonsXbox> actualActionStack;

    public int ActualIndex { get => actualIndex; set => actualIndex = value; }
    public bool DoingCombo { get => doingCombo; set => doingCombo = value; }
    public bool CanCancelCombo { get => canCancelCombo; set => canCancelCombo = value; }

    //TODO
    //Quizás sea necesario crear una lista de estas corrutinas.
    Coroutine cancelComboAfterTime;

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


    /*
     * 1- Aprieto botón bueno: Se inicia corrutina de cancelación
     *      2A- Aprieto botón continuación combo: Se cancela corrutina
     *      2B- Aprieto botón malo: Se cancela corrutina y se cancela el combo
     *      2C- No aprieto nada durante el tiempo de la corrutina: Se cancela el combo.
     *      
     */

    public void CancelCombo()
    {
        Debug.Log("#COMBO# Combo Canceled");
        DoingCombo = false;
        comboStarted = false;
        ActualIndex = 0;
        actualActionStack.Clear();
    }

    public void FinishCombo()
    {
        StartCoroutine(FinishingCombo());
    }


    IEnumerator FinishingCombo()
    {
        StopCoroutine(cancelComboAfterTime);
        DoingCombo = false;
        comboStarted = false;
        ActualIndex = 0;

        canAttack = false;
        yield return new WaitForSeconds(0.5f);
        canAttack = true;

    }

    
    //TODO
    //Cambiar esta función por un animationEvent que invoque el ResetCombo cuando corresponda.
    IEnumerator CancelComboAfterTime()
    {
        Debug.Log("#COMBO# Starting Cancel Coroutine");
        float timeBetweenAtacks = 0.0f;

        while(timeBetweenAtacks <= 0.5f)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            timeBetweenAtacks += Time.deltaTime;
        }

        if(timeBetweenAtacks >= 0.5f)
        {
            CancelCombo();
        }

        Debug.Log("#COMBO# Coroutine ended");
        
    }

    public void DoCombo(ButtonsXbox buttonPressed)
    {
        Debug.Log("Button pressed: " + buttonPressed);
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
            
            Debug.Log("Button: " + actualActionStack[0]);
        }
        
    }
    protected void StartCombo(ButtonsXbox buttonPressed)
    {
        for (int i = 0; i < model.BasicComboDefinitions.Length; ++i)
        {
            if (model.BasicComboDefinitions[i].StartCombo(buttonPressed))
            {
                comboStarted = true;
                cancelComboAfterTime = StartCoroutine(CancelComboAfterTime());
                NextComboStep(buttonPressed);
                break;
            }
        }

    }
    private void ContinueCombo(ButtonsXbox buttonPressed)
    {
        //TODO

        for(int i = 0; i < model.BasicComboDefinitions.Length; ++i)
        {
            if (model.BasicComboDefinitions[i].ContinueCombo(buttonPressed, actualActionStack))
            {
                //cancelamos la corrutina y empezamos otra
                StopCoroutine(cancelComboAfterTime);
                cancelComboAfterTime = StartCoroutine(CancelComboAfterTime());
                NextComboStep(buttonPressed);
                break;
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
        isCharging = false;
        actualTimeCharging = 0;
    }

    /*public async Task StartCharging()
    {

    }*/

    public IEnumerator StartCharging()
    {
        if(this.ActualIndex == 0)
        {
            isCharging = true;
            while (isCharging && actualTimeCharging < model.MaxTimeCharge)
            {
                actualTimeCharging += Time.deltaTime;
                Debug.Log("Time recharging = " + actualTimeCharging);
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }
        

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
