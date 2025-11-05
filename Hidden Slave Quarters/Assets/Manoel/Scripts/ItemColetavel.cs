using UnityEngine;

public class ItemColetavel : MonoBehaviour
{
    public string itemID;
    public string descricao;

    public void Coletar()
    {
       
        if (SistemaEntrega.instancia != null)
        {
            SistemaEntrega.instancia.PegarItem(itemID);
            GerenciadorMissoes.instancia.CompletarObjetivo("COLETOU_" + itemID);
            gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("SistemaEntrega não encontrado!");
        }
    }
}