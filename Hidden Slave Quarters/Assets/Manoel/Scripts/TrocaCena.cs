using UnityEngine;
using UnityEngine.SceneManagement;

public class TrocaCena : MonoBehaviour
{
    public string nomeDaCena;

    public void MudarCena()
    {
        if (string.IsNullOrEmpty(nomeDaCena)) return;
        SceneManager.LoadScene(nomeDaCena);
    }

    public void MudarCenaPara(string cena)
    {
        if (string.IsNullOrEmpty(cena)) return;
        SceneManager.LoadScene(cena);
    }
}
