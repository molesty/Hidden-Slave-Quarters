using UnityEngine;

public class CursorInterativo : MonoBehaviour
{
    public Texture2D cursorNormal;
    public Texture2D cursorInterativo;
    public Texture2D cursorAndar;

    void Start()
    {
        Cursor.SetCursor(cursorNormal, Vector2.zero, CursorMode.Auto);
    }

    void Update()
    {
        Vector3 posicaoMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        posicaoMouse.z = 0;

        RaycastHit2D hit = Physics2D.Raycast(posicaoMouse, Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.GetComponent<ItemColetavel>() != null ||
                hit.collider.GetComponent<NPCController>() != null ||
                hit.collider.GetComponent<ObjetoInterativo>() != null ||
                hit.collider.GetComponent<PistaAmbiente>() != null)
            {
                Cursor.SetCursor(cursorInterativo, Vector2.zero, CursorMode.Auto);
            }
            else
            {
                Cursor.SetCursor(cursorAndar, Vector2.zero, CursorMode.Auto);
            }
        }
        else
        {
            Cursor.SetCursor(cursorAndar, Vector2.zero, CursorMode.Auto);
        }
    }
}