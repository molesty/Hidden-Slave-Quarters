using UnityEngine;

public class SistemaClique : MonoBehaviour
{
    [Header("Referencias")]
    public Camera cameraPrincipal;

    [Header("Configuracoes")]
    public float distanciaMaxima = 10f;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ProcessarClique();
        }
    }

    void ProcessarClique()
    {
        Vector3 mouseWorld = cameraPrincipal.ScreenToWorldPoint(Input.mousePosition);
        Vector2 point2D = new Vector2(mouseWorld.x, mouseWorld.y);

        Collider2D hit = Physics2D.OverlapPoint(point2D);
        if (hit != null)
        {
            Debug.Log("Clicou em: " + hit.gameObject.name);
            VerificarInteracao(hit.gameObject);
        }
    }

    void VerificarInteracao(GameObject objeto)
    {
        PortalMudancaCena portal = objeto.GetComponent<PortalMudancaCena>();
        if (portal != null)
        {
            portal.AtivarPortal();
            return;
        }

        ItemColetavel item = objeto.GetComponent<ItemColetavel>();
        if (item != null)
        {
            item.Coletar();
            return;
        }

        AcaoEspecial acao = objeto.GetComponent<AcaoEspecial>();
        if (acao != null)
        {
            acao.ExecutarAcao();
            return;
        }
    }
}
