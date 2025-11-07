using System; 
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MudarDiaScript : MonoBehaviour
{
    public void MudarDia()
    {
        StartCoroutine(MudarDiaSeguroCoroutine());
    }

    IEnumerator MudarDiaSeguroCoroutine()
    {
        string cenaDestino = "SenzalaEvento";
        Debug.Log("MudarDia: iniciado -> " + cenaDestino);

        if (System.Type.GetType("SceneFader") != null)
        {
            var faderField = typeof(SceneFader).GetField("instancia");
            if (faderField != null)
            {
                var fader = SceneFader.instancia;
                if (fader != null)
                {
                    Debug.Log("MudarDia: usando SceneFader.instancia.FadeToScene");
                    fader.FadeToScene(cenaDestino);
                    yield break;
                }
                else Debug.Log("MudarDia: SceneFader.instancia == null");
            }
        }

        if (GameManager.instancia != null)
        {
            var metodo = GameManager.instancia.GetType().GetMethod("MudarCena");
            if (metodo != null)
            {
                Debug.Log("MudarDia: chamando GameManager.instancia.MudarCena");
                metodo.Invoke(GameManager.instancia, new object[] { cenaDestino });
                yield break;
            }
            else Debug.Log("MudarDia: GameManager.instancia existe mas não tem MudarCena()");
        }
        else Debug.Log("MudarDia: GameManager.instancia == null");

        if (!SceneExistsInBuild(cenaDestino))
        {
            Debug.LogError("MudarDia: cena '" + cenaDestino + "' NÃO está nas Build Settings. Vá em File > Build Settings e adicione a cena.");
            yield break;
        }

        Debug.Log("MudarDia: usando SceneManager.LoadScene fallback");
        AsyncOperation op = SceneManager.LoadSceneAsync(cenaDestino);
        while (!op.isDone) yield return null;
    }

    bool SceneExistsInBuild(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            string name = System.IO.Path.GetFileNameWithoutExtension(path);
            if (name == sceneName) return true;
        }
        return false;
    }
}
