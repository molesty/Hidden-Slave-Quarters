using UnityEngine;

public class PlayerItemCollector : MonoBehaviour
{
    public KeyCode collectKey = KeyCode.E; // tecla para coletar
    public float collectRange = 1.5f; // dist�ncia m�xima para coletar
    public LayerMask itemLayer; // layer dos itens

    void Update()
    {
        if (Input.GetKeyDown(collectKey))
        {
            CollectItem();
        }
    }

    void CollectItem()
    {
        // Detecta itens pr�ximos dentro do raio
        Collider2D item = Physics2D.OverlapCircle(transform.position, collectRange, itemLayer);

        if (item != null)
        {
            Debug.Log("Item coletado: " + item.name);
            Destroy(item.gameObject); // remove o item da cena
        }
    }

    void OnDrawGizmosSelected()
    {
        // Mostra o raio de coleta no editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, collectRange);
    }
}