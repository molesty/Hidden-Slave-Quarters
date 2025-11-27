using UnityEngine;
using TMPro;

public class chave : MonoBehaviour
{
    public portao gate;        
    public TMP_Text textoUI;   

    private void OnMouseDown()
    {
        gate.OpenGate();

        textoUI.text = "Você encontrou a chave e fugiu!";

        gameObject.SetActive(false);
    }
}
