using UnityEngine;

[CreateAssetMenu(fileName = "New Key", menuName = "Inventory/Key Item")]
public class KeyItem : ScriptableObject
{
    public string keyName;
    public Sprite keyIcon;
    //add more data here, like a description, a 3D model prefab, etc.
}
