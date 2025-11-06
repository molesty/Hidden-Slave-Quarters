using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;


public class FerroPickup_Senzala : MonoBehaviour
{
    [Header("Config")]
    public string nomeMissao = "Encontrar Ferro";
    public string mensagem = "Você encontrou um ferro enferrujado.";

    public void Interagir() => Pickup();
    public void OnInteract() => Pickup();
    public void Coletar() => Pickup();

    void Pickup()
    {
        if (SenzalaProgress.temFerro) return;

        SenzalaProgress.temFerro = true;
        if (GerenciadorMissoes.instancia != null) GerenciadorMissoes.instancia.CompletarMissao(nomeMissao);

        if (SistemaMensagens.instancia != null) SistemaMensagens.instancia.MostrarMensagem(mensagem);
        else Debug.Log(mensagem);

        gameObject.SetActive(false);
    }
}
