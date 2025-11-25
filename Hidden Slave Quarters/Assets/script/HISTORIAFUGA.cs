using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StoryText : MonoBehaviour
{
    public TextMeshProUGUI textUI;
    public float typingSpeed = 0.03f;
    public string[] storyLines;
    public string nextSceneName;

    void Start()
    {
        StartCoroutine(PlayStory());
    }

    IEnumerator PlayStory()
    {
        textUI.text = "";

        // Cada linha da história aparece como texto rolando
        foreach (string line in storyLines)
        {
            yield return StartCoroutine(TypeLine(line));
            yield return new WaitForSeconds(1f); // espera antes da próxima linha
        }

        // quando terminar, troca de cena
        SceneManager.LoadScene(nextSceneName);
    }

    IEnumerator TypeLine(string line)
    {
        textUI.text = "";
        foreach (char c in line.ToCharArray())
        {
            textUI.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
