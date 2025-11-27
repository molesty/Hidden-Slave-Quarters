using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class SenzalaEvento : MonoBehaviour
{
    public Image telaFade;
    public TMP_Text textoDialogo;
    public float duracaoCena = 5f;
    public float duracaoFade = 2f;

    void Start()
    {
        if (telaFade == null || textoDialogo == null)
        {
            Debug.LogWarning("Faltando referência de telaFade ou textoDialogo!");
            return;
        }

        telaFade.color = new Color(0, 0, 0, 0);
        textoDialogo.text = "";
        StartCoroutine(RodarCena());
    }

    IEnumerator RodarCena()
    {
        yield return new WaitForSeconds(0.5f);
        textoDialogo.text = "Ei... eu vou te tirar daqui, espere mais um pouco.";
        yield return new WaitForSeconds(duracaoCena - duracaoFade);
        yield return StartCoroutine(FadeOut());
        SceneManager.LoadScene("Senzala2");
    }

    IEnumerator FadeOut()
    {
        float tempo = 0f;
        Color cor = telaFade.color;

        while (tempo < duracaoFade)
        {
            tempo += Time.deltaTime;
            cor.a = Mathf.Lerp(0, 1, tempo / duracaoFade);
            telaFade.color = cor;
            yield return null;
        }
    }
}
