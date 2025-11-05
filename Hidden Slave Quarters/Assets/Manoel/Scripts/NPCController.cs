using UnityEngine;
using System.Collections;

public class NPCController : MonoBehaviour
{
    public string npcID;
    public string dialogo;
    public string itemQueRecebe;
    public string itemQueEntrega;

    public void Interagir()
    {
        if (SistemaEntrega.instancia.temItem)
        {
            if (SistemaEntrega.instancia.itemNaMao == itemQueRecebe)
            {
                SistemaEntrega.instancia.EntregarItem(npcID);

                if (!string.IsNullOrEmpty(itemQueEntrega))
                {
                    StartCoroutine(ProcessarEntrega());
                }
            }
        }
        else
        {
            UIManager.instancia.MostrarMensagem(dialogo);
        }
    }

    System.Collections.IEnumerator ProcessarEntrega()
    {
        UIManager.instancia.MostrarMensagem("Processando...");
        yield return new WaitForSeconds(5f);
        SistemaEntrega.instancia.PegarItem(itemQueEntrega);
    }
}