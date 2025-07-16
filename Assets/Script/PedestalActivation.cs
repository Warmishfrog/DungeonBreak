using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestalActivation : MonoBehaviour, IInteractable
{
    public UserInterfaceText UIText;

    public GameObject parkourObjects;

    private ILockable Lock;

    public void Start()
    {
        Lock = GetComponent<ILockable>(); // Can be null if no lock

    }

    /// <summary>
    /// This method would be called by player's interaction script
    /// </summary>
    public void Interact()
    {
        if (Lock != null && Lock.IsLocked)
        {
            TryUnlock();

            return;
        }
        else
        {
            Activate();
        }
    }

    public bool TryUnlock()
    {
        if (Lock == null)
            return true;

        bool unlocked = Lock.TryUnlock();
        if (unlocked) Activate();        
        else
        {
            string msg = Lock.RequiredKey != null ?
                $"This Lock requires the {Lock.RequiredKey.keyName}." :
                "This Lock can't be opened.";
            UIText.ShowText(msg);
        }

        return unlocked;
    }

    public void Activate() 
    { 
        // Implement the activation logic here
        string msg = "Pedestal Activated.";
        Debug.Log(msg);
        UIText.ShowText(msg);

        parkourObjects.SetActive(true);
    }
}
