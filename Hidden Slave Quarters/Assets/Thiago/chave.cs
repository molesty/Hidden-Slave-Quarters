using UnityEngine;

public class chave : MonoBehaviour
{
    public portâo gate; // referência ao portão

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gate.OpenGate();
            Destroy(gameObject); // remove a chave
        }
    }
}
