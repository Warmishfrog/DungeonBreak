using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserInterfaceText : MonoBehaviour
{
    public TextMeshProUGUI tmpText;
    public string text;
    public float fadeDuration = 0.5f;

    void Start()
    {
        ShowText(text);
    }

    public void ShowText(string newText, float displayDuration = 1f)
    {
        if (tmpText == null)
        {
            Debug.LogWarning("TMP Text reference is not assigned.");
            return;
        }

        tmpText.text = newText;
        tmpText.alpha = 1f;
        StopAllCoroutines();
        StartCoroutine(FadeOutAfterDelay(displayDuration));
    }

    private IEnumerator FadeOutAfterDelay(float displayDuration)
    {
        yield return new WaitForSeconds(displayDuration);

        float elapsed = 0f;
        Color originalColor = tmpText.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            tmpText.alpha = alpha;
            yield return null;
        }

        tmpText.alpha = 0f;
        tmpText.text = "";
    }
}
