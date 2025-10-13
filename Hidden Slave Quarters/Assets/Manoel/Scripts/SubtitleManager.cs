using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SubtitleManager : MonoBehaviour
{
    public Text subtitleText; // ou TMP_Text
    Coroutine current;

    public void Show(string text, float duration)
    {
        if (current != null) StopCoroutine(current);
        current = StartCoroutine(ShowRoutine(text, duration));
    }

    IEnumerator ShowRoutine(string text, float duration)
    {
        subtitleText.text = text;
        subtitleText.canvasRenderer.SetAlpha(1f);
        yield return new WaitForSeconds(duration);
        subtitleText.CrossFadeAlpha(0f, 0.25f, false);
    }

    public void Clear()
    {
        if (current != null) StopCoroutine(current);
        subtitleText.text = "";
    }
}
