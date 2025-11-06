using UnityEngine;
using Debug = UnityEngine.Debug;

public class PickupTool_Farm : MonoBehaviour
{
    public string nomeMissao = "Achar Ferramenta";
    public string mensagem = "Você achou uma ferramenta útil.";

    public void Interagir() => Pickup();

    void Pickup()
    {
        if (FazendaProgress.temFerramenta) return;

        FazendaProgress.temFerramenta = true;
        GerenciadorMissoes.instancia?.CompletarMissao(nomeMissao);
        if (SistemaMensagens.instancia != null) SistemaMensagens.instancia.MostrarMensagem(mensagem);
        else Debug.Log(mensagem);

        gameObject.SetActive(false);
    }
}
