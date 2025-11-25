using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class TrocaCena : MonoBehaviour
{
    public string nomeDaCena;
    public CanvasGroup painelCG;
    public Image fundoImagem;
    public TMP_Text tituloTexto;
    public float fadeIn = 0.4f;
    public float hold = 1.6f;
    public float fadeOut = 0.4f;
    public Sprite fundoCapitulo;
    public string tituloCapitulo;

    public void MudarCena()
    {
        if (nomeDaCena == "") return;
        StartCoroutine(Trocar(nomeDaCena));
    }

    public void MudarCenaPara(string cena)
    {
        if (cena == "") return;
        StartCoroutine(Trocar(cena));
    }

    IEnumerator Trocar(string cena)
    {
        if (painelCG != null)
        {
            painelCG.gameObject.SetActive(true);
            painelCG.alpha = 0f;
            fundoImagem.sprite = fundoCapitulo;
            tituloTexto.text = tituloCapitulo;

            float t = 0f;
            while (t < fadeIn)
            {
                t += Time.unscaledDeltaTime;
                painelCG.alpha = Mathf.Lerp(0f, 1f, t / fadeIn);
                yield return null;
            }

            painelCG.alpha = 1f;
            yield return new WaitForSecondsRealtime(hold);

            t = 0f;
            while (t < fadeOut)
            {
                t += Time.unscaledDeltaTime;
                painelCG.alpha = Mathf.Lerp(1f, 0f, t / fadeOut);
                yield return null;
            }
        }

        SceneManager.LoadScene(cena);
    }
}
