using UnityEngine;
using TMPro;

public class pedra : MonoBehaviour
{
    public GameObject keyHidden;
    public portao gate;
    public TMP_Text textoUI;

    private bool used = false;

    private void OnMouseDown()
    {
        if (used) return;


        keyHidden.SetActive(true);

        var sr = keyHidden.GetComponent<SpriteRenderer>();
        sr.sortingOrder = 999;  

        gameObject.SetActive(false);

        gate.OpenGate();

        textoUI.text = "Você encontrou a chave e fugiu!";

        used = true;
    }
}
