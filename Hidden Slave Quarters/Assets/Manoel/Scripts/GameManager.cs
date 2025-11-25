using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;

    public enum EstadoJogo { Menu, Jogando, Pausado, Cutscene }
    public EstadoJogo estadoAtual = EstadoJogo.Jogando;

    public int diaAtual = 1;
    public bool manterEntreCenas = true;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            if (manterEntreCenas) DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void AvancarDia()
    {
        diaAtual++;
        Debug.Log("GameManager: avançou para o dia " + diaAtual);
    }

    public void DefinirDia(int novoDia)
    {
        diaAtual = novoDia;
        Debug.Log("GameManager: dia definido para " + diaAtual);
    }

    public void MudarCena(string nomeCena)
    {
        if (string.IsNullOrEmpty(nomeCena)) return;
        if (SceneFader.instancia != null)
        {
            SceneFader.instancia.FadeToScene(nomeCena);
            return;
        }
        SceneManager.LoadScene(nomeCena);
    }

    public void ResetProgresso()
    {
        diaAtual = 1;
        Debug.Log("GameManager: progresso resetado (apenas dia)");
    }

    [ContextMenu("ImprimirStatus")]
    public void ImprimirStatus()
    {
        Debug.Log($"Status -> Dia:{diaAtual} Cena:{SceneManager.GetActiveScene().name}");
    }
}
