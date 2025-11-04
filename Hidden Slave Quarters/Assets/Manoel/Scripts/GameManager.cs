using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;

    public enum EstadoJogo
    {
        Jogando,
        Pausado,
        GameOver,
        MissaoCompleta
    }

    public EstadoJogo estadoAtual = EstadoJogo.Jogando;

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

    void Update()
    {
        GerenciarInputPausa();
    }

    void GerenciarInputPausa()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AlternarPausa();
        }
    }

    public void AlternarPausa()
    {
        if (estadoAtual == EstadoJogo.Jogando)
        {
            estadoAtual = EstadoJogo.Pausado;
            Time.timeScale = 0f;
            Debug.Log("Jogo pausado");
        }
        else if (estadoAtual == EstadoJogo.Pausado)
        {
            estadoAtual = EstadoJogo.Jogando;
            Time.timeScale = 1f;
            Debug.Log("Jogo despausado");
        }
    }

    public void JogadorCapturado()
    {
        estadoAtual = EstadoJogo.GameOver;
        Debug.Log("Jogador capturado! Game Over");

        if (UIManager.instancia != null)
        {
            UIManager.instancia.MostrarGameOver();
        }

        Invoke("RecarregarCena", 3f);
    }

    public void MissaoCompleta()
    {
        estadoAtual = EstadoJogo.MissaoCompleta;
        Debug.Log("Missão completa!");

        if (UIManager.instancia != null)
        {
            UIManager.instancia.MostrarMissaoCompleta();
        }
    }

    void RecarregarCena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        estadoAtual = EstadoJogo.Jogando;
        Time.timeScale = 1f;
    }

    public void SairParaMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuPrincipal");
    }
}