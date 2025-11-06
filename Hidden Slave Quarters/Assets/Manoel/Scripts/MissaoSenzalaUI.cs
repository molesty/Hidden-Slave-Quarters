using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;


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
        else Debug.LogWarning("MissaoSenzalaUI: botaoFerro não atribuído no Inspector.");

        if (botaoChave != null) botaoChave.onClick.AddListener(PuxarChave);
        else Debug.LogWarning("MissaoSenzalaUI: botaoChave não atribuído no Inspector.");

        if (botaoPorta != null) botaoPorta.onClick.AddListener(AbrirPorta);
        else Debug.LogWarning("MissaoSenzalaUI: botaoPorta não atribuído no Inspector.");
    }

    void ColetarFerro()
    {
        if (!ferroEncontrado)
        {
            ferroEncontrado = true;

            if (GerenciadorMissoes.instancia != null)
                GerenciadorMissoes.instancia.CompletarMissao("Encontrou o ferro");
            else
                Debug.LogWarning("GerenciadorMissoes.instancia está nulo.");

            if (SistemaMensagens.instancia != null)
                SistemaMensagens.instancia.MostrarMensagem("Você encontrou um ferro velho.");
            else
                Debug.Log("Você encontrou um ferro velho. (SistemaMensagens ausente)");

            if (botaoFerro != null) botaoFerro.interactable = false;
        }
    }

    void PuxarChave()
    {
        if (ferroEncontrado && !chavePuxada)
        {
            chavePuxada = true;

            if (GerenciadorMissoes.instancia != null)
                GerenciadorMissoes.instancia.CompletarMissao("Puxou a chave");
            else
                Debug.LogWarning("GerenciadorMissoes.instancia está nulo.");

            if (SistemaMensagens.instancia != null)
                SistemaMensagens.instancia.MostrarMensagem("Você conseguiu puxar uma chave com o ferro.");
            else
                Debug.Log("Você conseguiu puxar uma chave com o ferro. (SistemaMensagens ausente)");

            if (botaoChave != null) botaoChave.interactable = false;
        }
        else if (!ferroEncontrado)
        {
            if (SistemaMensagens.instancia != null)
                SistemaMensagens.instancia.MostrarMensagem("Você precisa de algo para puxar a chave.");
            else
                Debug.Log("Você precisa de algo para puxar a chave. (SistemaMensagens ausente)");
        }
    }

    void AbrirPorta()
    {
        if (chavePuxada)
        {
            if (GerenciadorMissoes.instancia != null)
                GerenciadorMissoes.instancia.CompletarMissao("Destrancou a porta");
            else
                Debug.LogWarning("GerenciadorMissoes.instancia está nulo.");

            if (SistemaMensagens.instancia != null)
                SistemaMensagens.instancia.MostrarMensagem("A porta se abriu. Você pode sair.");
            else
                Debug.Log("A porta se abriu. Você pode sair. (SistemaMensagens ausente)");

            if (GameManager.instancia != null)
                GameManager.instancia.MudarCena("Fazenda");
            else
                Debug.LogWarning("GameManager.instancia está nulo. Não foi possível mudar de cena.");
        }
        else
        {
            if (SistemaMensagens.instancia != null)
                SistemaMensagens.instancia.MostrarMensagem("A porta está trancada.");
            else
                Debug.Log("A porta está trancada. (SistemaMensagens ausente)");
        }
    }
}
