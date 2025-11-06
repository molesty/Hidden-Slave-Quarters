using UnityEngine;

public class TrocaDeCena : MonoBehaviour
{
    public string nomeCenaDestino;
    public string mensagemBloqueada = "A saída está bloqueada, termine as missões.";

    public void TentarMudarCena()
    {
        if (GerenciadorMissoes.instancia != null && !GerenciadorMissoes.instancia.TodasConcluidas())
        {
            UIManager.instancia?.Mostrar(mensagemBloqueada);
            return;
        }

        UIManager.instancia?.Mostrar("Saindo...");
        GameManager.instancia?.MudarCena(nomeCenaDestino);
    }
}
