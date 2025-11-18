using UnityEngine;
using UnityEngine.UI;

public class MissaoFazenda : MonoBehaviour
{
    public Button botaoFerramenta;
    public Button botaoBalde;
    public Button botaoPoco;
    public Button ponto1;
    public Button ponto2;
    public Button ponto3;
    public Button botaoMudarDia;

    public GameObject pocoQuebrado;
    public GameObject pocoArrumado;

    public GameObject baldeVazio;
    public GameObject baldeCheio;

    bool pegouFerramenta = false;
    bool pegouBalde = false;
    bool pocoArrumadoFlag = false;
    bool temAgua = false;
    bool regou1 = false;
    bool regou2 = false;
    bool regou3 = false;

    void Start()
    {
        if (botaoFerramenta != null) botaoFerramenta.onClick.AddListener(PegarFerramenta);
        if (botaoBalde != null) botaoBalde.onClick.AddListener(PegarBalde);
        if (botaoPoco != null) botaoPoco.onClick.AddListener(AcaoPoco);
        if (ponto1 != null) ponto1.onClick.AddListener(() => Regar(1));
        if (ponto2 != null) ponto2.onClick.AddListener(() => Regar(2));
        if (ponto3 != null) ponto3.onClick.AddListener(() => Regar(3));
        if (botaoMudarDia != null) botaoMudarDia.onClick.AddListener(MudarDia);

        if (botaoMudarDia != null) botaoMudarDia.gameObject.SetActive(false);

        AtualizarPoço();
        AtualizarBalde();
        AtualizarInteracoes();
    }

    void PegarFerramenta()
    {
        if (pegouFerramenta) return;
        pegouFerramenta = true;
        FazendaProgress.temFerramenta = true;
        if (botaoFerramenta != null) botaoFerramenta.interactable = false;
        SistemaMensagens.instancia?.MostrarMensagem("Você pegou um ferro");
        VerificarLiberar();
    }

    void PegarBalde()
    {
        if (pegouBalde) return;
        pegouBalde = true;
        FazendaProgress.temBalde = true;
        if (botaoBalde != null) botaoBalde.interactable = false;
        SistemaMensagens.instancia?.MostrarMensagem("Você pegou o balde");
        AtualizarBalde();
        VerificarLiberar();
    }

    void AcaoPoco()
    {
        if (!pegouFerramenta)
        {
            SistemaMensagens.instancia?.MostrarMensagem("Você precisa arrumar o poço!");
            return;
        }

        if (!pocoArrumadoFlag)
        {
            pocoArrumadoFlag = true;
            FazendaProgress.pocoArrumado = true;
            AtualizarPoço();
            SistemaMensagens.instancia?.MostrarMensagem("Você arrumou o poço");
            VerificarLiberar();
            return;
        }

        if (!pegouBalde)
        {
            SistemaMensagens.instancia?.MostrarMensagem("Pegue o balde primeiro");
            return;
        }

        temAgua = true;
        FazendaProgress.temAgua = true;
        AtualizarBalde();
        SistemaMensagens.instancia?.MostrarMensagem("Você encheu o balde");
        AtualizarInteracoes();
    }

    void Regar(int i)
    {
        if (!pegouBalde)
        {
            SistemaMensagens.instancia?.MostrarMensagem("Você precisa do balde");
            return;
        }

        if (!temAgua)
        {
            SistemaMensagens.instancia?.MostrarMensagem("O balde está vazio");
            return;
        }

        if (i == 1 && !regou1) regou1 = true;
        else if (i == 2 && !regou2) regou2 = true;
        else if (i == 3 && !regou3) regou3 = true;
        else return;

        temAgua = false;
        FazendaProgress.temAgua = false;
        AtualizarBalde();
        SistemaMensagens.instancia?.MostrarMensagem("Você regou a plantação");
        AtualizarInteracoes();
        VerificarLiberar();
    }

    void AtualizarPoço()
    {
        if (pocoQuebrado != null) pocoQuebrado.SetActive(!pocoArrumadoFlag);
        if (pocoArrumado != null) pocoArrumado.SetActive(pocoArrumadoFlag);
    }

    void AtualizarBalde()
    {
        if (!pegouBalde)
        {
            if (baldeVazio != null) baldeVazio.SetActive(true);
            if (baldeCheio != null) baldeCheio.SetActive(false);
            return;
        }

        if (baldeVazio != null) baldeVazio.SetActive(!temAgua);
        if (baldeCheio != null) baldeCheio.SetActive(temAgua);
    }

    void AtualizarInteracoes()
    {
        bool pode = temAgua;
        if (ponto1 != null) ponto1.interactable = !regou1 && pode;
        if (ponto2 != null) ponto2.interactable = !regou2 && pode;
        if (ponto3 != null) ponto3.interactable = !regou3 && pode;
    }

    void VerificarLiberar()
    {
        AtualizarPoço();
        AtualizarBalde();
        AtualizarInteracoes();

        if (pegouFerramenta && pegouBalde && pocoArrumadoFlag && regou1 && regou2 && regou3)
        {
            if (botaoMudarDia != null) botaoMudarDia.gameObject.SetActive(true);
            FazendaProgress.liberto = true;
        }
    }

    void MudarDia()
    {
        if (!(pegouFerramenta && pegouBalde && pocoArrumadoFlag && regou1 && regou2 && regou3))
        {
            SistemaMensagens.instancia?.MostrarMensagem("Ainda falta completar tudo");
            return;
        }

        if (SceneFader.instancia != null)
        {
            SceneFader.instancia.FadeToScene("Senzala2");
            return;
        }

        if (GameManager.instancia != null)
        {
            GameManager.instancia.MudarCena("Senzala2");
            return;
        }
    }
}
