using UnityEngine;

public class pedra : MonoBehaviour
{
    public GameObject keyHidden;
    public portâo gate;

    private bool used = false;

    private void OnMouseDown()
    {
        if (used) return;
        gameObject.SetActive(false);
        keyHidden.SetActive(true);
        var sr = keyHidden.GetComponent<SpriteRenderer>();
        sr.sortingOrder = 10;

        gate.OpenGate();

        used = true;
    }
}
