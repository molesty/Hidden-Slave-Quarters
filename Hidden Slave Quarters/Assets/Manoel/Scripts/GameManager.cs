using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;
    public int diaAtual = 1;

    public void ProximoDia()
    {
        diaAtual++;
        if (diaAtual == 2)
            MudarCena("Senzala"); // volta pra senzala no segundo dia
        else if (diaAtual > 2)
            MudarCena("Final"); // ou termina o jogo
    }

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
