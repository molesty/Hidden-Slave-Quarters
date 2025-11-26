using UnityEngine;
using System.Collections;

public class SistemaClique : MonoBehaviour
{
    private Camera cameraPrincipal;

    void Start()
    {
        StartCoroutine(InicializarClique());
    }

    IEnumerator InicializarClique()
    {

        yield return null;


        cameraPrincipal = Camera.main;

        if (cameraPrincipal == null)
            Debug.LogError("SistemaClique: Nenhuma câmera principal encontrada na cena!");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (cameraPrincipal == null) return;


            Vector3 mouseWorld = cameraPrincipal.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, -cameraPrincipal.transform.position.z)
            );
            Vector2 p = new Vector2(mouseWorld.x, mouseWorld.y);


            Collider2D hit = Physics2D.OverlapPoint(p);
            if (hit != null)
            {

                ObjetoInterativo obj = hit.GetComponent<ObjetoInterativo>();
                if (obj != null)
                    obj.Interagir();
            }
        }
    }
}
