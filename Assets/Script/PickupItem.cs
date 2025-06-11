using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour, IInteractable
{

    [SerializeField] private UserInterfaceText UIText;

    public void Interact()
    {

        Debug.Log("picked up");
        UIText.SetText("picked up");

        //add item to inventory

        Destroy(gameObject); // Destroy the item after picking it up
    }
}
