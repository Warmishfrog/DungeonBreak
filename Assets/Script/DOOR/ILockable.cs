using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILockable
{
    bool IsLocked { get; }
    KeyItem RequiredKey { get; }


    bool TryUnlock();
    void EnableLock();
}
