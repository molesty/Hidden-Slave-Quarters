using UnityEngine;
using TMPro;

public class chave : MonoBehaviour
{
    public portao gate;        // referência ao portão
    public TMP_Text textoUI;   // referência ao texto na tela

    private void OnMouseDown()
    {
        gate.OpenGate();

        textoUI.text = "Você encontrou a chave e fugiu!";

        gameObject.SetActive(false);
    }
}
