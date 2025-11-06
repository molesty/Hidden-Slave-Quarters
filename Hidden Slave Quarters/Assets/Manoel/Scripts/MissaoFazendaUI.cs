using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class MissaoFazendaUI : MonoBehaviour
{
    public Button botaoAgua;
    public Button botaoFerramenta;
    public Button botaoSair;

    private bool aguaPega = false;
    private bool ferramentaPega = false;

    void Start()
    {
        if (botaoAgua != null) botaoAgua.onClick.AddListener(PegarAgua);
        if (botaoFerramenta != null) botaoFerramenta.onClick.AddListener(PegarFerramenta);
        if (botaoSair != null) botaoSair.onClick.AddListener(TentarSair);
        AtualizarBotaoSair();
    }

    void PegarAgua()
    {
        if (aguaPega)
        {
            SistemaMensagens.instancia?.MostrarMensagem("Você já pegou água.");
            return;
        }

        aguaPega = true;
        SistemaMensagens.instancia?.MostrarMensagem("Você pegou um balde de água.");
        botaoAgua.interactable = false;
        AtualizarBotaoSair();
    }

    void PegarFerramenta()
    {
        if (ferramentaPega)
        {
            SistemaMensagens.instancia?.MostrarMensagem("Você já pegou a ferramenta.");
            return;
        }

        ferramentaPega = true;
        SistemaMensagens.instancia?.MostrarMensagem("Você achou uma ferramenta útil.");
        botaoFerramenta.interactable = false;
        AtualizarBotaoSair();
    }

    void TentarSair()
    {
        if (!aguaPega || !ferramentaPega)
        {
            SistemaMensagens.instancia?.MostrarMensagem("Ainda falta algo para levar.");
            return;
        }

        SistemaMensagens.instancia?.MostrarMensagem("Você concluiu tudo, saindo...");
        if (GameManager.instancia != null)
            GameManager.instancia.MudarCena("ProximaCena");
        else
            Debug.LogWarning("GameManager.instancia não encontrado!");
    }

    void AtualizarBotaoSair()
    {
        if (botaoSair == null) return;
        botaoSair.interactable = aguaPega && ferramentaPega;
    }
}
