using UnityEngine;

public class SistemaEntrega : MonoBehaviour
{
    public static SistemaEntrega instancia;

    public string itemNaMao;
    public bool temItem;

    void Awake()
    {
        instancia = this;
    }

    public void PegarItem(string itemID)
    {
        itemNaMao = itemID;
        temItem = true;

        if (UIManager.instancia != null)
        {
            UIManager.instancia.MostrarMensagem("Pegou: " + itemID);
        }
    }

    public void EntregarItem(string npcID)
    {
        if (temItem)
        {
            GerenciadorMissoes.instancia.CompletarObjetivo("ENTREGUE_" + itemNaMao + "_PARA_" + npcID);
            itemNaMao = "";
            temItem = false;
        }
    }

    public void UsarItem(string localID)
    {
        if (temItem)
        {
            GerenciadorMissoes.instancia.CompletarObjetivo("USOU_" + itemNaMao + "_EM_" + localID);
            itemNaMao = "";
            temItem = false;
        }
    }
}