using UnityEngine;
using Debug = UnityEngine.Debug;

public class GerenciadorMissoes : MonoBehaviour
{
    public static GerenciadorMissoes instancia;
    public int totalMissoes = 3;
    private int missoesCompletas = 0;

    void Awake()
    {
        if (instancia == null) instancia = this;
        else Destroy(gameObject);
    }

    public void CompletarMissao(string nome)
    {
        missoesCompletas++;

        if (UIManager.instancia != null)
        {
            UIManager.instancia.Mostrar("Missão concluída: " + nome);
        }

        Debug.Log("Missão concluída: " + nome + " (" + missoesCompletas + "/" + totalMissoes + ")");
    }

    public bool TodasConcluidas() => missoesCompletas >= totalMissoes;
}
