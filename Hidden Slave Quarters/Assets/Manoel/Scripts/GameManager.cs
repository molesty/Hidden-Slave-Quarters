using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;

    [Header("Progresso do Jogo")]
    public bool temChaveSenzala = false;
    public bool temFerramentaFazenda = false;
    public bool derrotouGuardas = false;

    [Header("Configuracoes")]
    public string proximaCena;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void MudarCena(string nomeCena)
    {
        SceneManager.LoadScene(nomeCena);
    }

    public void CompletarMissao(string missao)
    {
        Debug.Log("Missao completada: " + missao);
    }
}
