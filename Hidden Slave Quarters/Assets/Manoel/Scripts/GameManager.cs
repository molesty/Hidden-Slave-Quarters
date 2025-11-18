using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;

    public enum EstadoJogo { Menu, Jogando, Pausado, Cutscene }
    public EstadoJogo estadoAtual = EstadoJogo.Jogando;

    [Header("Progresso (Senzala / Fazenda)")]
    public bool ferroColetado = false;
    public bool chaveColetada = false;
    public bool derrotouGuardas = false;

    [Header("Dia / Cena")]
    public int diaAtual = 1;
    public string cenaInicial = "";

    [Header("Opções")]
    public bool manterEntreCenas = true;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
            return;
        }
    }

    public void ColetouFerro()
    {
        ferroColetado = true;
        Debug.Log("GameManager: ferro coletado");
        SistemaMensagens.instancia?.MostrarMensagem("Você pegou o ferro.");
    }

    public void ColetouChave()
    {
        chaveColetada = true;
        Debug.Log("GameManager: chave coletada");
        SistemaMensagens.instancia?.MostrarMensagem("Você pegou a chave.");
    }

    public void SetDerrotouGuardas(bool valor = true)
    {
        derrotouGuardas = valor;
        Debug.Log("GameManager: derrotouGuardas = " + derrotouGuardas);
    }

    public bool PodeAbrirPorta()
    {
        bool pode = ferroColetado && chaveColetada;
        Debug.Log("GameManager: PodeAbrirPorta -> " + pode);
        return pode;
    }

    public void AbrirPorta(string nomeCenaDestino)
    {
        if (PodeAbrirPorta())
        {
            Debug.Log("GameManager: Abrindo porta e mudando para cena " + nomeCenaDestino);
            SistemaMensagens.instancia?.MostrarMensagem("A porta abriu.");
            MudarCena(nomeCenaDestino);
        }
        else
        {
            Debug.Log("GameManager: porta trancada, faltam itens");
            SistemaMensagens.instancia?.MostrarMensagem("A porta está trancada.");
        }
    }

    public void MudarCena(string nomeCena)
    {
        if (string.IsNullOrEmpty(nomeCena))
        {
            Debug.LogWarning("GameManager.MudarCena: nome de cena vazio.");
            return;
        }

        Debug.Log("GameManager: Mudando para cena: " + nomeCena);
        SceneManager.LoadScene(nomeCena);
    }

    public void AvancarDia()
    {
        diaAtual++;
        Debug.Log("GameManager: avançou para o dia " + diaAtual);
    }

    public void MudarDia(int novoDia)
    {
        diaAtual = novoDia;
        Debug.Log("GameManager: dia definido para " + diaAtual);
    }

    public void ResetProgresso()
    {
        ferroColetado = false;
        chaveColetada = false;
        derrotouGuardas = false;
        Debug.Log("GameManager: progresso resetado");
    }

    [ContextMenu("ImprimirStatus")]
    public void ImprimirStatus()
    {
        Debug.Log($"Status -> Ferro:{ferroColetado} Chave:{chaveColetada} Guardas:{derrotouGuardas} Dia:{diaAtual} Cena:{SceneManager.GetActiveScene().name}");
    }
}