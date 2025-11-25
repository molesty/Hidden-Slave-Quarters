using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class ChapterIntro : MonoBehaviour
{
    public static ChapterIntro instancia;

    public CanvasGroup painelCG;
    public Image backgroundImage;
    public TMP_Text tituloText;
    public float fadeIn = 0.4f;
    public float hold = 1.6f;
    public float fadeOut = 0.4f;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
            if (painelCG != null) painelCG.alpha = 0f;
            gameObject.SetActive(true);
        }
        else Destroy(gameObject);
    }

    public void ShowChapterAndLoad(string titulo, Sprite bgSprite, float totalHoldSeconds, string sceneToLoad)
    {
        StopAllCoroutines();
        StartCoroutine(ShowAndLoadCoroutine(titulo, bgSprite, totalHoldSeconds, sceneToLoad));
    }

    IEnumerator ShowAndLoadCoroutine(string titulo, Sprite bgSprite, float totalHoldSeconds, string sceneToLoad)
    {
        if (tituloText != null) tituloText.text = titulo;
        if (backgroundImage != null) backgroundImage.sprite = bgSprite;
        if (painelCG == null)
        {
            if (!string.IsNullOrEmpty(sceneToLoad)) SceneManager.LoadScene(sceneToLoad);
            yield break;
        }

        float halfHold = Mathf.Max(0, totalHoldSeconds);
        float t = 0f;
        while (t < fadeIn)
        {
            t += Time.unscaledDeltaTime;
            painelCG.alpha = Mathf.Lerp(0f, 1f, t / fadeIn);
            yield return null;
        }
        painelCG.alpha = 1f;

        yield return new WaitForSecondsRealtime(halfHold);

        t = 0f;
        while (t < fadeOut)
        {
            t += Time.unscaledDeltaTime;
            painelCG.alpha = Mathf.Lerp(1f, 0f, t / fadeOut);
            yield return null;
        }
        painelCG.alpha = 0f;

        if (!string.IsNullOrEmpty(sceneToLoad))
            SceneManager.LoadScene(sceneToLoad);
    }
}
