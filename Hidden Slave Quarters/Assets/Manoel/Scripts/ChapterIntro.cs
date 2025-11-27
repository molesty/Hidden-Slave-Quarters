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

    public bool autoShowOnLoad = true;
    public bool skipFirstSceneLoad = true;

    bool _firstLoadSeen = false;

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

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!autoShowOnLoad) return;
        if (skipFirstSceneLoad && !_firstLoadSeen)
        {
            _firstLoadSeen = true;
            return;
        }

        string title = HumanizeSceneName(scene.name);
        Sprite bg = backgroundImage != null ? backgroundImage.sprite : null;

        ShowChapterAndLoad(title, bg, hold, null);
    }

    string HumanizeSceneName(string name)
    {
        if (name.ToLower().Contains("senzala")) return "A Senzala";
        if (name.ToLower().Contains("fazenda1")) return "Fazenda - Colheita";
        if (name.ToLower().Contains("fazenda")) return "A Fazenda";
        return name;
    }

    public void ShowChapterAndLoad(string titulo, Sprite bgSprite, float totalHoldSeconds, string sceneToLoad)
    {
        StopAllCoroutines();
        StartCoroutine(ShowAndMaybeLoadCoroutine(titulo, bgSprite, totalHoldSeconds, sceneToLoad));
    }

    IEnumerator ShowAndMaybeLoadCoroutine(string titulo, Sprite bgSprite, float totalHoldSeconds, string sceneToLoad)
    {
        if (tituloText != null) tituloText.text = titulo;
        if (backgroundImage != null && bgSprite != null) backgroundImage.sprite = bgSprite;

        if (painelCG == null)
        {
            if (!string.IsNullOrEmpty(sceneToLoad)) SceneManager.LoadScene(sceneToLoad);
            yield break;
        }

        float t = 0f;
        while (t < fadeIn)
        {
            t += Time.unscaledDeltaTime;
            painelCG.alpha = Mathf.Lerp(0f, 1f, t / fadeIn);
            yield return null;
        }
        painelCG.alpha = 1f;

        float elapsed = 0f;
        while (elapsed < totalHoldSeconds)
        {
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

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
