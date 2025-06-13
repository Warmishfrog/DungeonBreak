using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

interface IInteractable
{
    void Interact();
}

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform interactorSource;
    [SerializeField] private float interactRange;
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [SerializeField] private LayerMask interactableLayers;
    [SerializeField] private UserInterfaceText UIPrompt;

    private IInteractable currentInteractable;

    void Update()
    {
        CheckForInteractable(); // always run this to update UI

        if (Input.GetKeyDown(interactKey) && currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }


    private void CheckForInteractable()
    {
        Ray ray = new Ray(interactorSource.position, interactorSource.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, interactRange, interactableLayers))
        {
            if (hit.collider.TryGetComponent<IInteractable>(out var interactable))
            {
                currentInteractable = interactable;
                UIPrompt.SetText($"Press {interactKey} to interact");
                return;
            }
        }

        currentInteractable = null;
        UIPrompt.SetText(""); // Clear the UI prompt if no interactable is found
    }

    private void OnDrawGizmosSelected()
    {
        if (interactorSource != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(interactorSource.position, interactorSource.forward * interactRange);
        }
    }
}
