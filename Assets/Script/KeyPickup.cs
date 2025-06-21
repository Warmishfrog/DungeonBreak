using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour, IInteractable
{
    public KeyItem key;

    [SerializeField] private UserInterfaceText UIText;

    public void Interact()
    {
        UIText.ShowText($"picked up {key}");

        //add item to inventory
        Inventory.instance.AddKey(key);

        gameObject.SetActive(false); // Destroy the item after picking it up
    }
}
