using UnityEngine;
using UnityEngine.UI;

public class MissaoSenzalaUI : MonoBehaviour
{
    [Header("Botões da Senzala")]
    public Button botaoFerro;
    public Button botaoChave;
    public Button botaoPorta;

    private bool ferroEncontrado = false;
    private bool chavePuxada = false;

    void Start()
    {
        if (botaoFerro != null) botaoFerro.onClick.AddListener(ColetarFerro);
        if (botaoChave != null) botaoChave.onClick.AddListener(PuxarChave);
        if (botaoPorta != null) botaoPorta.onClick.AddListener(AbrirPorta);
    }

    void ColetarFerro()
    {
        if (!ferroEncontrado)
        {
            ferroEncontrado = true;
            if (botaoFerro != null) botaoFerro.interactable = false;
            SistemaMensagens.instancia?.MostrarMensagem("Você encontrou um ferro velho.");
            GerenciadorMissoes.instancia?.CompletarMissao("Encontrou o ferro");
        }
        else
        {
            SistemaMensagens.instancia?.MostrarMensagem("Você já pegou o ferro.");
        }
    }

    void PuxarChave()
    {
        if (!ferroEncontrado)
        {
            SistemaMensagens.instancia?.MostrarMensagem("Muito alto, não consigo alcançar", 1.5f);
            return;
        }

        if (!chavePuxada)
        {
            chavePuxada = true;
            if (botaoChave != null) botaoChave.interactable = false;
            SistemaMensagens.instancia?.MostrarMensagem("Você conseguiu puxar uma chave com o ferro.");
            GerenciadorMissoes.instancia?.CompletarMissao("Puxou a chave");
            SenzalaProgress.temChave = true;
        }
        else
        {
            SistemaMensagens.instancia?.MostrarMensagem("Você já pegou a chave.");
        }
    }

    void AbrirPorta()
    {
        if (!chavePuxada)
        {
            SistemaMensagens.instancia?.MostrarMensagem("A porta está trancada.");
            return;
        }

        SistemaMensagens.instancia?.MostrarMensagem("A porta se abriu. Você pode sair.");
        GerenciadorMissoes.instancia?.CompletarMissao("Destrancou a porta");

        if (GameManager.instancia != null)
            GameManager.instancia.MudarCena("Fazenda");
        else
            Debug.LogWarning("GameManager.instancia não encontrado!");
    }
}
