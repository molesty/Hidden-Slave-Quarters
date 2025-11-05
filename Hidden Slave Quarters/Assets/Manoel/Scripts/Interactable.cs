using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public string tooltipText;

    // chamada quando clicado
    public abstract void OnInteract();
}
