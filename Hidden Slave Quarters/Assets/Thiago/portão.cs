using UnityEngine;

public class portâo : MonoBehaviour
{
    public Sprite gateClosed;
    public Sprite gateOpen;
    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = gateClosed;
    }

    public void OpenGate()
    {
        sr.sprite = gateOpen;
    }
}
