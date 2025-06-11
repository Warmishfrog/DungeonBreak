using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractMessager : MonoBehaviour, IInteractable
{

    [SerializeField] private UserInterfaceText UIText;
    public string InteractMessage = "InteractMessage";

    public void Interact()
    {
        Debug.Log(InteractMessage);
        UIText.SetText(InteractMessage);

    }

    public float RandomNumber()
    {
        return Random.Range(0, 100);
    }
}
