using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void Quit()
    {
        Debug.Log("Saindo do jogo...");
        Application.Quit();
    }
}