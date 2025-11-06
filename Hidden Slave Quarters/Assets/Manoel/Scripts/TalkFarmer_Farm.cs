using UnityEngine;
using Debug = UnityEngine.Debug;

public class TalkFarmer_Farm : MonoBehaviour
{
    public string nomeMissao = "Falar com o Fazendeiro";
    public string mensagem = "O fazendeiro te agradece e te dá instruções.";

    public void Interagir() => Talk();

    void Talk()
    {
        if (FazendaProgress.falouComFazendeiro) return;

        FazendaProgress.falouComFazendeiro = true;
        GerenciadorMissoes.instancia?.CompletarMissao(nomeMissao);
        if (SistemaMensagens.instancia != null) SistemaMensagens.instancia.MostrarMensagem(mensagem);
        else Debug.Log(mensagem);
    }
}
