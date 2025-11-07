using System.Collections;
using UnityEngine;

public class ControladorDia : MonoBehaviour
{
    public static ControladorDia instancia;
    public int diaAtual = 1;
    public bool diaEmAndamento;

    void Awake()
    {
        instancia = this;
    }

    void Start()
    {
        StartCoroutine(InicioDiaSeguro());
    }

    IEnumerator InicioDiaSeguro()
    {
        yield return new WaitForSeconds(1f);
        if (GameManager.instancia == null)
        {
            Debug.LogWarning("ControladorDia: GameManager.instancia nulo (usando dia 1).");
        }
        else
        {
            GameManager.instancia.AtualizarDia(diaAtual);
        }
        diaEmAndamento = true;
    }

    public void FimDia()
    {
        if (!diaEmAndamento) return;
        diaEmAndamento = false;
        StartCoroutine(FimDiaSeguro());
    }

    IEnumerator FimDiaSeguro()
    {
        yield return new WaitForSeconds(1f);
        if (GameManager.instancia == null)
        {
            Debug.Log("ControladorDia: avançaria o dia, mas GameManager.instancia é nulo.");
        }
        else
        {
            diaAtual++;
            GameManager.instancia.AtualizarDia(diaAtual);
        }
        diaEmAndamento = true;
    }
}
