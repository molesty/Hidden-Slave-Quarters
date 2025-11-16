using UnityEngine;
using UnityEngine.SceneManagement;

public class TrocaCena : MonoBehaviour
{
    public string nomeDaCena;

    public void MudarCena()
    {
        if (nomeDaCena == "") return;
        SceneManager.LoadScene(nomeDaCena);
    }

    public void MudarCenaPara(string cena)
    {
        if (cena == "") return;
        SceneManager.LoadScene(cena);
    }
}
