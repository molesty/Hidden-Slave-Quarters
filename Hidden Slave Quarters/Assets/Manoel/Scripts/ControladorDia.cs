using UnityEngine;
using TMPro;

public class ControladorDia : MonoBehaviour
{
    public CanvasGroup fade;
    public TMP_Text textoDia;

    void Start()
    {
        StartCoroutine(InicioDia());
    }

    System.Collections.IEnumerator InicioDia()
    {
        textoDia.text = "Dia " + GameManager.instancia.diaAtual;
        fade.alpha = 1;
        yield return new WaitForSeconds(1.5f);
        while (fade.alpha > 0)
        {
            fade.alpha -= Time.deltaTime;
            yield return null;
        }
    }

    public void TerminarTrabalho()
    {
        StartCoroutine(FimDia());
    }

    System.Collections.IEnumerator FimDia()
    {
        textoDia.text = "Anoitecendo...";
        while (fade.alpha < 1)
        {
            fade.alpha += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(1.5f);
        GameManager.instancia.ProximoDia();
    }
}
