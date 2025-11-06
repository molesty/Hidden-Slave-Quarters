using UnityEngine;
using Debug = UnityEngine.Debug;

public class PickupWater_Farm : MonoBehaviour
{
    public string nomeMissao = "Pegar Água";
    public string mensagem = "Você pegou um balde de água.";

    public void Interagir() => Pickup();

    void Pickup()
    {
        if (FazendaProgress.temAgua) return;

        FazendaProgress.temAgua = true;
        GerenciadorMissoes.instancia?.CompletarMissao(nomeMissao);
        if (SistemaMensagens.instancia != null) SistemaMensagens.instancia.MostrarMensagem(mensagem);
        else Debug.Log(mensagem);

        gameObject.SetActive(false);
    }
}
