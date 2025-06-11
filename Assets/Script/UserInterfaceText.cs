using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserInterfaceText : MonoBehaviour
{
    public TextMeshProUGUI tmpText;
    public string text;

    void Start()
    {
        SetText(text);
    }

    public void SetText(string newText)
    {
        if (tmpText != null)
        {
            tmpText.text = newText;
        }
        else
        {
            Debug.LogWarning("TMP Text reference is not assigned.");
        }
    }
}
