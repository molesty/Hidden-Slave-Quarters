using UnityEngine;

public class ItemColetavel : MonoBehaviour
{
    public string itemID;
    public string descricao;
    public int pontosConhecimento = 10;

    public void Coletar()
    {
        if (SistemaEntrega.instancia != null)
        {
            SistemaEntrega.instancia.PegarItem(itemID);
        }

        GerenciadorMissoes.instancia.CompletarObjetivo("COLETOU_" + itemID);

        GerenciadorClicks.instancia.AdicionarConhecimento(pontosConhecimento);

        UIManager.instancia.MostrarMensagem("Coletou: " + descricao);

        gameObject.SetActive(false);
    }
}