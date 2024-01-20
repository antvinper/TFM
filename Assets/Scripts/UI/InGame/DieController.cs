using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DieController : MonoBehaviour
{
    [SerializeField] private Button goToLobbyButton;

    public void Setup(EventSystem eventSystem)
    {
        eventSystem.SetSelectedGameObject(goToLobbyButton.gameObject);
    }
}
