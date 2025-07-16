using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Singleton instance
    public static Inventory instance;

    // Use a HashSet for efficient "Contains" checks
    [SerializeField]private HashSet<KeyItem> keys = new HashSet<KeyItem>();

    void Awake()
    {
        // Set up the Singleton
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject); // Optional: keep inventory between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddKey(KeyItem key)
    {
        if (keys.Add(key))
        {
            Debug.Log("Picked up " + key.keyName);
            // You can fire an event here for the UI to update
        }
    }

    public bool HasKey(KeyItem key)
    {
        return key != null && keys.Contains(key);
    }
}
