using UnityEngine;
using UnityEngine.SceneManagement;

public class CliqueParaCena : MonoBehaviour
{
    public string nomeDaCena; // cena para carregar

    private void OnMouseDown()
    {
        StartCoroutine(TrocarDepois());
    }

    private System.Collections.IEnumerator TrocarDepois()
    {
        yield return new WaitForSeconds(2f); // aguarda 3 segundos
        SceneManager.LoadScene(nomeDaCena);
    }
}
