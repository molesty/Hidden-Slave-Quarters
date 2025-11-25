using UnityEngine;

public class pedra : MonoBehaviour
{
    public GameObject keyHidden;
    public portâo gate;

    private bool used = false;

    private void OnMouseDown()
    {
        if (used) return;

        // remove a pedra
        gameObject.SetActive(false);

        // ativa a chave
        keyHidden.SetActive(true);

        // coloca a chave na frente
        var sr = keyHidden.GetComponent<SpriteRenderer>();
        sr.sortingOrder = 10;

        // abre o portão
        gate.OpenGate();

        used = true;
    }
}
