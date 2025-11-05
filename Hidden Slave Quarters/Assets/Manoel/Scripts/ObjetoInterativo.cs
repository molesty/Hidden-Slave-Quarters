using UnityEngine;

public class ObjetoInterativo : MonoBehaviour
{
    public string triggerMissao;
    public string mensagemInteracao;
    public int pontosConhecimento = 5;

    public void Interagir()
    {
        if (!string.IsNullOrEmpty(triggerMissao))
        {
            GerenciadorMissoes.instancia.CompletarObjetivo(triggerMissao);
        }

        if (!string.IsNullOrEmpty(mensagemInteracao))
        {
            UIManager.instancia.MostrarMensagem(mensagemInteracao);
        }

        if (pontosConhecimento > 0)
        {
            GerenciadorClicks.instancia.AdicionarConhecimento(pontosConhecimento);
        }
    }
}