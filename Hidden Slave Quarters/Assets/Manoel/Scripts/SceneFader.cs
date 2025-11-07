using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    public static SceneFader instancia;
    public CanvasGroup fadePanel;
    public float velocidade = 2f;

    void Awake()
    {
        if (instancia == null) instancia = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void FadeToScene(string nomeCena)
    {
        StartCoroutine(FadeAndLoad(nomeCena));
    }

    IEnumerator FadeAndLoad(string nomeCena)
    {
        if (fadePanel != null)
        {
            fadePanel.blocksRaycasts = true;
            while (fadePanel.alpha < 1f) { fadePanel.alpha += Time.deltaTime * velocidade; yield return null; }
        }

        AsyncOperation op = SceneManager.LoadSceneAsync(nomeCena);
        while (!op.isDone) yield return null;

        if (fadePanel != null)
        {
            while (fadePanel.alpha > 0f) { fadePanel.alpha -= Time.deltaTime * velocidade; yield return null; }
            fadePanel.blocksRaycasts = false;
        }
    }
}
