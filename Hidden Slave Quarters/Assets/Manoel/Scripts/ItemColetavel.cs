using UnityEngine;

public class ItemColetavel : MonoBehaviour
{
    [Header("Configuracao do Item")]
    public string nomeItem;
    public string tipoItem;
    public string mensagemColeta;

    [Header("Efeitos")]
    public bool desapareceAposColeta = true;

    public void Coletar()
    {
        switch (tipoItem)
        {
            case "ChaveSenzala":
                GameManager.instancia.temChaveSenzala = true;
                break;
            case "Ferramenta":
                GameManager.instancia.temFerramentaFazenda = true;
                break;
        }

        InventoryManager.instancia?.AdicionarItem(tipoItem, nomeItem);

        SistemaMensagens.instancia?.MostrarMensagem(mensagemColeta);

        if (desapareceAposColeta)
        {
            gameObject.SetActive(false);
        }
    }
}
