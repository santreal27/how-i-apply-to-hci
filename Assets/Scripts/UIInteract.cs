using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class UIInteract : MonoBehaviour
{
    [SerializeField] private GameObject promptInteractObject;

    [SerializeField] UnityEvent OnInteract;

    
    private void OnEnable()
    {
        PlayerController.OnInteract += Interact;
        PlayerController.OnEnterInteractable += ShowUI;
        PlayerController.OnExitInteractable += HideUI;
    }

    private void OnDisable()
    {
        PlayerController.OnInteract -= Interact;
        PlayerController.OnEnterInteractable -= ShowUI;
        PlayerController.OnExitInteractable -= HideUI;
    }

    void Interact()
    {
        Debug.Log("Interacting");
        //if ()
       // OnInteract.Invoke();
    }
    void ShowUI()
    {
        promptInteractObject.SetActive(true);
    }

    void HideUI()
    {
        promptInteractObject.SetActive(false);
    }
}
