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

        // ativa a chave exatamente onde ela já está
        keyHidden.SetActive(true);

        // pega o sprite da chave e coloca NA FRENTE da pedra
        var sr = keyHidden.GetComponent<SpriteRenderer>();
        sr.sortingOrder = 999;  // bem na frente

        // remove a pedra
        gameObject.SetActive(false);

        // abre o portão
        gate.OpenGate();

        // mostra o texto
        textoUI.text = "Você encontrou a chave e fugiu!";

        used = true;
    }
}
