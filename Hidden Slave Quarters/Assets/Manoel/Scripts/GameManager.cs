using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;

    public enum EstadoJogo { Jogando, Pausado }
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

    public void MudarCena(string nomeCena)
    {
        SceneManager.LoadScene(nomeCena);
    }
}
