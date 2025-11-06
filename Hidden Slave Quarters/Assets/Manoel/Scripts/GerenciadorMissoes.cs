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

    public void CompletarMissao(string nomeMissao)
    {
        missoesCompletas++;
        UIManager.instancia?.Mostrar("Missão concluída: " + nomeMissao);
        Debug.Log("Missão concluída: " + nomeMissao + " (" + missoesCompletas + "/" + totalMissoes + ")");
    }

    public bool TodasConcluidas()
    {
        return missoesCompletas >= totalMissoes;
    }

    public int GetCompletas() { return missoesCompletas; }
}
