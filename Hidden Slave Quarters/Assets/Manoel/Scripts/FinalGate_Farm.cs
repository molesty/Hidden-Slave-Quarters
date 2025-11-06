using UnityEngine;

public class FinalGate_Farm : MonoBehaviour
{
    public GameObject portaVisual;
    public string nomeCenaDestino = "ProximaCena";

    void Start()
    {
        AtualizarEstado();
    }

    void Update()
    {
        AtualizarEstado();
    }

    void AtualizarEstado()
    {
        if (portaVisual != null)
            portaVisual.SetActive(GerenciadorMissoes.instancia != null && GerenciadorMissoes.instancia.TodasConcluidas());
    }

    public void TentarAbrirPorta()
    {
        if (GerenciadorMissoes.instancia != null && !GerenciadorMissoes.instancia.TodasConcluidas())
        {
            SistemaMensagens.instancia?.MostrarMensagem("Você não pode sair ainda.");
            return;
        }

        SistemaMensagens.instancia?.MostrarMensagem("Saindo...");
        if (GameManager.instancia != null) GameManager.instancia.MudarCena(nomeCenaDestino);
    }
}
