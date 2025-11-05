using UnityEngine;

public class GerenciadorClicks : MonoBehaviour
{
    public static GerenciadorClicks instancia;

    [Header("Configuracoes")]
    public int conhecimentoCultural = 0;

    void Awake()
    {
        instancia = this;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ProcessarClick();
        }
    }

    void ProcessarClick()
    {
        Vector2 posicaoMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(posicaoMouse, Vector2.zero);

        if (hit.collider != null)
        {
            Debug.Log("Clicou em: " + hit.collider.gameObject.name);

            ItemColetavel item = hit.collider.GetComponent<ItemColetavel>();
            if (item != null)
            {
                item.Coletar();
                return;
            }

            NPCController npc = hit.collider.GetComponent<NPCController>();
            if (npc != null)
            {
                npc.Interagir();
                return;
            }

            ObjetoInterativo obj = hit.collider.GetComponent<ObjetoInterativo>();
            if (obj != null)
            {
                obj.Interagir();
                return;
            }

            PistaAmbiente pista = hit.collider.GetComponent<PistaAmbiente>();
            if (pista != null)
            {
                pista.RevelarPista();
                return;
            }
        }
    }

    public void AdicionarConhecimento(int pontos)
    {
        conhecimentoCultural += pontos;
        if (UIManager.instancia != null)
        {
            UIManager.instancia.AtualizarUI();
        }
    }
}