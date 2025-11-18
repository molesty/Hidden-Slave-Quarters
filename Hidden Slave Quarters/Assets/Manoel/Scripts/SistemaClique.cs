using UnityEngine;

public class SistemaClique : MonoBehaviour
{
    public Camera cameraPrincipal;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorld = cameraPrincipal.ScreenToWorldPoint(Input.mousePosition);
            Vector2 p = new Vector2(mouseWorld.x, mouseWorld.y);

            Collider2D hit = Physics2D.OverlapPoint(p);
            if (hit != null)
            {
                ObjetoInterativo obj = hit.GetComponent<ObjetoInterativo>();
                if (obj != null)
                {
                    obj.Interagir();
                    return;
                }

                TrocaDeCena porta = hit.GetComponent<TrocaDeCena>();
                if (porta != null)
                {
                    porta.TentarMudarCena();
                    return;
                }

            }
        }
    }
}