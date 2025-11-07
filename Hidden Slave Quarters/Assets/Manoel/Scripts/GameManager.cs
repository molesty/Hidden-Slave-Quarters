using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;
    public int diaAtual = 1;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void AtualizarDia(int novoDia)
    {
        Debug.Log("Dia atual: " + novoDia);
    }


    public void ProximoDia()
    {
        diaAtual++;
        Debug.Log("GameManager: avançou para dia " + diaAtual);
    }

    public void MudarCena(string nomeCena)
    {
        if (string.IsNullOrEmpty(nomeCena))
        {
            Debug.LogWarning("GameManager.MudarCena: nome de cena vazio.");
            return;
        }
        SceneManager.LoadScene(nomeCena);
    }
}
