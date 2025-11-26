using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;

    public enum EstadoJogo { Menu, Jogando, Pausado, Cutscene }
    public EstadoJogo estadoAtual = EstadoJogo.Jogando;

    public int diaAtual = 1;
    public bool manterEntreCenas = true;

    [HideInInspector]
    public Missao missaoScript;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            if (manterEntreCenas)
                DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene cena, LoadSceneMode modo)
    {
        StartCoroutine(InicializarMissaoCena());
    }

    IEnumerator InicializarMissaoCena()
    {
        yield return null;

        missaoScript = Object.FindFirstObjectByType<Missao>();

        SistemaMensagens sistema = Object.FindFirstObjectByType<SistemaMensagens>();
        if (sistema != null)
            SistemaMensagens.instancia = sistema;

        if (missaoScript != null)
        {
            AtualizarMissaoPorCena();
        }
        else
        {
            Debug.LogWarning("GameManager: Nenhuma missão encontrada na cena.");
        }
    }

    public void AtualizarMissaoPorCena()
    {
        if (missaoScript == null) return;

        string nomeCena = SceneManager.GetActiveScene().name;

        switch (nomeCena)
        {
            case "Senzala":
                missaoScript.descricao = "Explore a senzala e encontre pistas sobre os escravizados.";
                break;
            case "Fazenda":
                missaoScript.descricao = "Regue a plantação e arrume o poço.";
                break;
            case "Fazenda1":
                missaoScript.descricao = "Colha a plantação e organize o poço.";
                break;
            case "Fuga1":
                missaoScript.descricao = "Encontre a saída e fuja sem ser visto.";
                break;
            default:
                missaoScript.descricao = "Explore a área.";
                break;
        }

        if (SistemaMensagens.instancia != null)
            SistemaMensagens.instancia.MostrarMensagem(missaoScript.descricao, 4f);
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
