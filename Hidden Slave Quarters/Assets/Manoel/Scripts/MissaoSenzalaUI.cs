using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MissaoSenzalaUI : MonoBehaviour
{
    public Button botaoFerro;
    public Button botaoChave;
    public Button botaoPorta;
    public GameObject novoFundo;

    private bool ferroEncontrado = false;
    private bool chavePuxada = false;

    void Start()
    {
        if (botaoFerro != null) botaoFerro.onClick.AddListener(ColetarFerro);
        if (botaoChave != null) botaoChave.onClick.AddListener(PuxarChave);
        if (botaoPorta != null) botaoPorta.onClick.AddListener(() => StartCoroutine(AbrirPorta()));
    }

    void ColetarFerro()
    {
        if (!ferroEncontrado)
        {
            ferroEncontrado = true;
            botaoFerro.interactable = false;
            SistemaMensagens.instancia?.MostrarMensagem("Você encontrou um ferro velho.");
        }
    }

    void PuxarChave()
    {
        if (!ferroEncontrado)
        {
            SistemaMensagens.instancia?.MostrarMensagem("Você precisa de algo para puxar a chave.");
            return;
        }

        if (!chavePuxada)
        {
            chavePuxada = true;
            botaoChave.interactable = false;
            SistemaMensagens.instancia?.MostrarMensagem("Você conseguiu puxar uma chave com o ferro.");
        }
        else
        {
            SistemaMensagens.instancia?.MostrarMensagem("Você já pegou a chave.");
        }
    }

    IEnumerator AbrirPorta()
    {
        if (!chavePuxada)
        {
            SistemaMensagens.instancia?.MostrarMensagem("A porta está trancada.");
            yield break;
        }

        SistemaMensagens.instancia?.MostrarMensagem("A porta se abriu. Você pode sair.");
        if (novoFundo != null)
        {
            novoFundo.SetActive(true);
            yield return new WaitForSeconds(1f);
        }

        if (GameManager.instancia != null)
            GameManager.instancia.MudarCena("Fazenda");
        else
            Debug.LogWarning("GameManager.instancia não encontrado!");
    }
}
