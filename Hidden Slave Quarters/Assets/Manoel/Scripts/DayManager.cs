using UnityEngine;
using System.Collections;

public class DayManager : MonoBehaviour
{
    public static DayManager instancia;
    public int diaAtual = 1;
    public int diasTotais = 2;
    public bool ehDia = true;
    public float tempoNoite = 2f;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    void Start()
    {
        ehDia = true;
        diaAtual = 1;
        SistemaMensagens.instancia?.MostrarMensagem("Dia " + diaAtual, 1.2f);
    }

    public void Dormir()
    {
        if (!ehDia) return;
        StartCoroutine(CicloDormir());
    }

    IEnumerator CicloDormir()
    {
        ehDia = false;
        SistemaMensagens.instancia?.MostrarMensagem("Noite... você dorme.", 1.2f);
        yield return new WaitForSeconds(tempoNoite);
        if (diaAtual < diasTotais) diaAtual++;
        SistemaMensagens.instancia?.MostrarMensagem("Dia " + diaAtual, 1.2f);
        ehDia = true;
    }
}
