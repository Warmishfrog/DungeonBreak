using System.Collections;
using System.Collections.Generic;
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

    void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            TryInteract();
        }        
    }

    private void TryInteract()
    {
        Ray ray = new Ray(interactorSource.position, interactorSource.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, interactRange, interactableLayers))
        {
            if (hit.collider.TryGetComponent<IInteractable>(out var interactable))
            {
                interactable.Interact();
            }
        }
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
