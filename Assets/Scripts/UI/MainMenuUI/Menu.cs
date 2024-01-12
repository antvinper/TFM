using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Menu: MonoBehaviour
{
    [Header("Main Menu Manager")]
    [SerializeField] protected MainMenuController mainMenuManager;
    public virtual void PerformAction(EventSystem eventSystem)
    {

    }
}
