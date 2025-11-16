using UnityEngine;
using UnityEngine.UI;

public class MissaoFazenda : MonoBehaviour
{
    public Button botaoFerramenta;
    public Button botaoBalde;
    public Button botaoPoco;
    public Button[] pontosPlanta;
    public Button botaoMudarDia;
    public string cenaSenzalaEvento;
    public int capacidadeAgua = 3;

    int regou = 0;
    int carga = 0;
    bool pegouFerramenta = false;
    bool pegouBalde = false;
    bool pocoArrumado = false;
    bool[] jaRegou;

    void Start()
    {
        if (pontosPlanta == null) pontosPlanta = new Button[0];
        jaRegou = new bool[pontosPlanta.Length];

        FazendaProgress.temAgua = false;
        FazendaProgress.temFerramenta = false;
        FazendaProgress.falouComFazendeiro = false;

        if (botaoFerramenta != null) botaoFerramenta.onClick.AddListener(PegarFerramenta);
        if (botaoBalde != null) botaoBalde.onClick.AddListener(PegarBalde);
        if (botaoPoco != null) botaoPoco.onClick.AddListener(AcaoPoco);

        for (int i = 0; i < pontosPlanta.Length; i++)
        {
            int id = i;
            if (pontosPlanta[i] != null)
            {
                pontosPlanta[i].onClick.RemoveAllListeners();
                pontosPlanta[i].onClick.AddListener(() => Regar(id));
            }
        }

        if (botaoMudarDia != null)
        {
            botaoMudarDia.onClick.RemoveAllListeners();
            botaoMudarDia.onClick.AddListener(MudarDia);
            botaoMudarDia.gameObject.SetActive(false);
            botaoMudarDia.interactable = false;
        }

        AtualizarUI();
    }

    void PegarFerramenta()
    {
        if (pegouFerramenta) { SistemaMensagens.instancia?.MostrarMensagem("Já pegou a ferramenta."); return; }
        pegouFerramenta = true;
        FazendaProgress.temFerramenta = true;
        if (botaoFerramenta != null) botaoFerramenta.interactable = false;
        SistemaMensagens.instancia?.MostrarMensagem("Você pegou a ferramenta.");
        AtualizarUI();
        VerificarCompleto();
    }

    void PegarBalde()
    {
        if (pegouBalde) { SistemaMensagens.instancia?.MostrarMensagem("Já pegou o balde."); return; }
        pegouBalde = true;
        if (botaoBalde != null) botaoBalde.interactable = false;
        SistemaMensagens.instancia?.MostrarMensagem("Você pegou o balde.");
        AtualizarUI();
        VerificarCompleto();
    }

    void AcaoPoco()
    {
        if (!pegouFerramenta)
        {
            SistemaMensagens.instancia?.MostrarMensagem("Você precisa da ferramenta para usar o poço.");
            return;
        }

        if (!pegouBalde)
        {
            SistemaMensagens.instancia?.MostrarMensagem("Você precisa do balde para tirar água.");
            return;
        }

        if (!pocoArrumado)
        {
            pocoArrumado = true;
            FazendaProgress.falouComFazendeiro = true;
            SistemaMensagens.instancia?.MostrarMensagem("Você arrumou o poço.");
            AtualizarUI();
            VerificarCompleto();
            return;
        }

        carga = capacidadeAgua;
        FazendaProgress.temAgua = true;
        SistemaMensagens.instancia?.MostrarMensagem("Você encheu o balde. Capacidade: " + carga);
        AtualizarUI();
    }

    void Regar(int i)
    {
        if (i < 0 || i >= pontosPlanta.Length) return;
        if (pontosPlanta[i] == null) return;
        if (jaRegou[i]) return;

        if (carga <= 0)
        {
            SistemaMensagens.instancia?.MostrarMensagem("O balde está vazio, encha no poço.");
            return;
        }

        jaRegou[i] = true;
        pontosPlanta[i].interactable = false;
        regou++;
        carga--;
        if (carga == 0) FazendaProgress.temAgua = false;
        SistemaMensagens.instancia?.MostrarMensagem("Você regou a planta. Água restante: " + carga);
        AtualizarUI();
        VerificarCompleto();
    }

    void VerificarCompleto()
    {
        bool todasRegadas = true;
        for (int i = 0; i < jaRegou.Length; i++) if (!jaRegou[i]) { todasRegadas = false; break; }

        if (pegouFerramenta && pegouBalde && pocoArrumado && todasRegadas)
        {
            if (botaoMudarDia != null)
            {
                botaoMudarDia.gameObject.SetActive(true);
                botaoMudarDia.interactable = true;
                SistemaMensagens.instancia?.MostrarMensagem("Tudo pronto, pode mudar o dia.");
            }
            FazendaProgress.liberto = true;
        }
    }

    void AtualizarUI()
    {
        if (botaoPoco != null)
        {
            botaoPoco.interactable = pegouFerramenta && pegouBalde;
        }

        for (int i = 0; i < pontosPlanta.Length; i++)
        {
            if (pontosPlanta[i] != null)
            {
                pontosPlanta[i].interactable = !jaRegou[i] && (carga > 0);
            }
        }
    }

    void MudarDia()
    {
        if (!(pegouFerramenta && pegouBalde && pocoArrumado && regou >= pontosPlanta.Length))
        {
            SistemaMensagens.instancia?.MostrarMensagem("Ainda falta completar as missões.");
            return;
        }

        if (SceneFader.instancia != null)
        {
            SceneFader.instancia.FadeToScene(cenaSenzalaEvento);
            return;
        }

        if (GameManager.instancia != null)
        {
            GameManager.instancia.MudarCena(cenaSenzalaEvento);
            return;
        }

        Debug.LogWarning("Nenhum sistema de troca de cena encontrado");
    }
}
