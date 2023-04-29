using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] WeaponModel weaponModel;

    private float actualTimeCharging = 0;
    private bool isCharging = false;

    private int ActualIndex = 0;
    private bool DoingCombo = false;
    private bool CanCancelCombo = false;

    //public int actualIndex { get => ActualIndex; }

    //Tengo que ponerla a false al terminar el ResetCombo
    bool comboStarted;

    public int actualIndex { get => ActualIndex; set => ActualIndex = value; }

    private bool canAttack;

    public bool doingCombo { get => DoingCombo; set => DoingCombo = value; }
    public bool canCancelCombo { get => CanCancelCombo; set => CanCancelCombo = value; }

    //TODO
    //Quizás sea necesario crear una lista de estas corrutinas.
    Coroutine cancelComboAfterTime;

    private void Start()
    {
        canAttack = true;
        foreach(BasicComboDefinition basicComboDefinition in weaponModel.basicComboDefinitions)
        {
            basicComboDefinition.SetUp(this);
        }
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
        doingCombo = false;
        comboStarted = false;
        actualIndex = 0;
    }

    public void FinishCombo()
    {
        StartCoroutine(FinishingCombo());
    }


    IEnumerator FinishingCombo()
    {
        StopCoroutine(cancelComboAfterTime);
        doingCombo = false;
        comboStarted = false;
        actualIndex = 0;

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
        if(canAttack)
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
    private void StartCombo(ButtonsXbox buttonPressed)
    {
        for (int i = 0; i < weaponModel.basicComboDefinitions.Length; ++i)
        {
            if (weaponModel.basicComboDefinitions[i].StartCombo(buttonPressed))
            {
                comboStarted = true;
                cancelComboAfterTime = StartCoroutine(CancelComboAfterTime());
                //StartCoroutine(weaponModel.basicComboDefinitions[i].CancelComboAfterTime());
            }
        }

    }
    private void ContinueCombo(ButtonsXbox buttonPressed)
    {
        //TODO

        for(int i = 0; i < weaponModel.basicComboDefinitions.Length; ++i)
        {
            if (weaponModel.basicComboDefinitions[i].ContinueCombo(buttonPressed))
            {
                //cancelamos la corrutina y empezamos otra
                StopCoroutine(cancelComboAfterTime);
                cancelComboAfterTime = StartCoroutine(CancelComboAfterTime());
                //StartCoroutine(weaponModel.basicComboDefinitions[i].CancelComboAfterTime());
            }
        }
    }
    

    public void StopCharging()
    {
        isCharging = false;
        actualTimeCharging = 0;
    }

    public IEnumerator StartCharging()
    {
        isCharging = true;
        while(isCharging && actualTimeCharging < weaponModel.maxTimeCharge)
        {
            actualTimeCharging += Time.deltaTime;
            Debug.Log("Time recharging = " + actualTimeCharging);
            yield return new WaitForSeconds(Time.deltaTime);
        }

    }
}
