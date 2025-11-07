using UnityEngine;
using UnityEngine.UI;

public class MissaoFazendaUI : MonoBehaviour
{
    public Button botaoFerramenta;
    public Button botaoBalde;
    public Button botaoMudarDia;
    public string cenaSenzalaEvento = "SenzalaEvento";

    bool pegouFerramenta = false;
    bool pegouBalde = false;

    void Start()
    {
        if (botaoFerramenta != null) botaoFerramenta.onClick.AddListener(PegarFerramenta);
        if (botaoBalde != null) botaoBalde.onClick.AddListener(PegarBalde);
        if (botaoMudarDia != null) botaoMudarDia.onClick.AddListener(MudarDia);

        if (botaoMudarDia != null) botaoMudarDia.gameObject.SetActive(false);
    }

    void PegarFerramenta()
    {
        if (pegouFerramenta) { SistemaMensagens.instancia?.MostrarMensagem("Já pegou a ferramenta."); return; }
        pegouFerramenta = true;
        botaoFerramenta.interactable = false;
        SistemaMensagens.instancia?.MostrarMensagem("Você achou uma ferramenta útil.", 1.5f);
        VerificarLiberarBotao();
    }

    void PegarBalde()
    {
        if (pegouBalde) { SistemaMensagens.instancia?.MostrarMensagem("Já pegou o balde."); return; }
        pegouBalde = true;
        botaoBalde.interactable = false;
        SistemaMensagens.instancia?.MostrarMensagem("Você pegou um balde de água.", 1.5f);
        VerificarLiberarBotao();
    }

    void VerificarLiberarBotao()
    {
        if (pegouFerramenta && pegouBalde)
        {
            if (botaoMudarDia != null) botaoMudarDia.gameObject.SetActive(true);
            SistemaMensagens.instancia?.MostrarMensagem("Tudo pronto, pode ir.", 1.5f);
        }
    }

    void MudarDia()
    {
        if (botaoMudarDia != null) botaoMudarDia.interactable = false;
        SceneFader.instancia?.FadeToScene(cenaSenzalaEvento);
    }
}
