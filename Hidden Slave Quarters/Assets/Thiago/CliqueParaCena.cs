using UnityEngine;
using UnityEngine.SceneManagement;

public class CliqueParaCena : MonoBehaviour
{
    public string nomeDaCena; 

    private void OnMouseDown()
    {
        StartCoroutine(TrocarDepois());
    }

    private System.Collections.IEnumerator TrocarDepois()
    {
        yield return new WaitForSeconds(2f); 
        SceneManager.LoadScene(nomeDaCena);
    }
}
