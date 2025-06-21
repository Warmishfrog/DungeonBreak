using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour, ILockable
{
    #nullable enable
    public KeyItem? requiredKey;
    public GameObject? LockVisual;
    #nullable disable

    public bool IsLocked { get; private set; } = true;
    public KeyItem RequiredKey => requiredKey;


    public bool TryUnlock()
    {
        if (Inventory.instance.HasKey(requiredKey))
        {
            Debug.Log("Unlocked.");
            if (LockVisual != null) LockVisual.SetActive(false);
            IsLocked = false;
            return true;
        }
        else
        {
            string msg = requiredKey != null ? $"Requires the {requiredKey.keyName}." : "Can't be unlocked.";
            Debug.Log(msg);
            return false;
        }
    }

    public void EnableLock()
    {
        IsLocked = true;
        if (LockVisual != null) LockVisual.SetActive(true);
    }
}
