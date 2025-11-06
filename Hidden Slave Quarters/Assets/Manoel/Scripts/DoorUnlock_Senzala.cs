using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;


public class DoorUnlock_Senzala : MonoBehaviour
{
    [Header("Config")]
    public string nomeCenaDestino = "Fazenda";
    public string nomeMissao = "Sair da senzala";
    public string mensagemBloqueada = "A porta está trancada. Precisa de uma chave.";
    public string mensagemAbrindo = "A porta abriu. Você segue para a fazenda...";

    public void Interagir() => TryOpen();
    public void OnInteract() => TryOpen();
    public void Coletar() => TryOpen();

    void TryOpen()
    {
        if (!SenzalaProgress.temChave)
        {
            if (SistemaMensagens.instancia != null) SistemaMensagens.instancia.MostrarMensagem(mensagemBloqueada);
            else Debug.Log(mensagemBloqueada);
            return;
        }

        SenzalaProgress.portaDestravada = true;

        if (GerenciadorMissoes.instancia != null) GerenciadorMissoes.instancia.CompletarMissao(nomeMissao);
        if (SistemaMensagens.instancia != null) SistemaMensagens.instancia.MostrarMensagem(mensagemAbrindo);
        else Debug.Log(mensagemAbrindo);

        if (GameManager.instancia != null)
        {
            GameManager.instancia.MudarCena(nomeCenaDestino);
        }
        else
        {
            Debug.Log("GameManager não encontrado — precisa configurar troca de cena manualmente.");
        }
    }
}
