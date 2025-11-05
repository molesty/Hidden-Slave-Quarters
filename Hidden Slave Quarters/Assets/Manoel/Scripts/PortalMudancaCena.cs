using UnityEngine;

public class PortalMudancaCena : MonoBehaviour
{
    [Header("Configuracao do Portal")]
    public string nomeDaCena;
    public bool requerItem;
    public string itemRequerido;

    [Header("Mensagens")]
    public string mensagemSucesso;
    public string mensagemFalha;

    public void AtivarPortal()
    {
        if (requerItem && !VerificarItem())
        {
            SistemaMensagens.instancia?.MostrarMensagem(mensagemFalha);
            return;
        }

        SistemaMensagens.instancia?.MostrarMensagem(mensagemSucesso);
        GameManager.instancia?.MudarCena(nomeDaCena);
    }

    bool VerificarItem()
    {
        switch (itemRequerido)
        {
            case "ChaveSenzala": return GameManager.instancia != null && GameManager.instancia.temChaveSenzala;
            case "Ferramenta": return GameManager.instancia != null && GameManager.instancia.temFerramentaFazenda;
            default: return true;
        }
    }
}
