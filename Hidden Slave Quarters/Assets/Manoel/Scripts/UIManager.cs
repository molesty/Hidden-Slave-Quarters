using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instancia;

    [Header("UI de Missoes")]
    public GameObject painelMissao;
    public TextMeshProUGUI textoTituloMissao;
    public TextMeshProUGUI textoDescricaoMissao;
    public Transform containerObjetivos;
    public GameObject prefabObjetivo;

    [Header("UI do Jogador")]
    public TextMeshProUGUI textoPontosConhecimento;
    public GameObject indicadorFurtivo;
    public GameObject indicadorEscondido;

    [Header("Sistema de Mensagens")]
    public GameObject painelMensagem;
    public TextMeshProUGUI textoMensagem;
    public float duracaoMensagem = 3f;

    [Header("UI de Game State")]
    public GameObject painelGameOver;
    public GameObject painelMissaoCompleta;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        EsconderTodosPaineis();
        AtualizarUI();
    }

    void Update()
    {
        AtualizarIndicadoresJogador();
    }

    public void MostrarMissao(GerenciadorMissoes.Missao missao)
    {
        if (missao == null) return;

        textoTituloMissao.text = missao.titulo;
        textoDescricaoMissao.text = missao.descricao;

        foreach (Transform child in containerObjetivos)
            Destroy(child.gameObject);

        foreach (var objetivo in missao.objetivos)
        {
            GameObject obj = Instantiate(prefabObjetivo, containerObjetivos);
            TextMeshProUGUI texto = obj.GetComponentInChildren<TextMeshProUGUI>();
            if (texto != null)
            {
                texto.text = objetivo.descricao;

                if (objetivo.isCompleted)
                {
                    texto.color = Color.green;
                    texto.text = "[X] " + texto.text;
                }
                else
                {
                    texto.color = Color.white;
                    texto.text = "[ ] " + texto.text;
                }
            }
        }

        painelMissao.SetActive(true);
        Invoke("EsconderPainelMissao", 5f);
    }

    public void UpdateQuestUI(GerenciadorMissoes.Missao missao)
    {
        MostrarMissao(missao);
    }

    void EsconderPainelMissao()
    {
        if (painelMissao != null)
            painelMissao.SetActive(false);
    }

    public void AtualizarUI()
    {
        // CORREÇÃO: Usar FindAnyObjectByType corretamente
        var jogador = FindAnyObjectByType<PersonagemController>();
        if (jogador != null && textoPontosConhecimento != null)
        {
            textoPontosConhecimento.text = "Conhecimento: " + jogador.conhecimentoCultural;
        }
    }

    void AtualizarIndicadoresJogador()
    {
        // CORREÇÃO: Usar FindAnyObjectByType corretamente
        var jogador = FindAnyObjectByType<PersonagemController>();
        if (jogador != null)
        {
            if (indicadorFurtivo != null)
                indicadorFurtivo.SetActive(Input.GetKey(KeyCode.LeftShift));

            if (indicadorEscondido != null)
                indicadorEscondido.SetActive(jogador.estaEscondido);
        }
    }

    public void MostrarMensagem(string mensagem)
    {
        if (textoMensagem != null)
        {
            textoMensagem.text = mensagem;
        }

        if (painelMensagem != null)
        {
            painelMensagem.SetActive(true);
        }

        CancelInvoke("EsconderMensagem");
        Invoke("EsconderMensagem", duracaoMensagem);
    }

    void EsconderMensagem()
    {
        if (painelMensagem != null)
            painelMensagem.SetActive(false);
    }

    public void MostrarGameOver()
    {
        if (painelGameOver != null)
            painelGameOver.SetActive(true);
    }

    public void MostrarMissaoCompleta()
    {
        if (painelMissaoCompleta != null)
            painelMissaoCompleta.SetActive(true);
    }

    public void EsconderTodosPaineis()
    {
        if (painelGameOver != null)
            painelGameOver.SetActive(false);

        if (painelMissaoCompleta != null)
            painelMissaoCompleta.SetActive(false);

        if (painelMensagem != null)
            painelMensagem.SetActive(false);
    }

    public void BotaoReiniciar()
    {
        if (GameManager.instancia != null)
        {
            GameManager.instancia.JogadorCapturado();
        }
    }

    public void BotaoContinuar()
    {
        Debug.Log("Continuar para proxima missao...");
    }

    public void BotaoMenuPrincipal()
    {
        if (GameManager.instancia != null)
        {
            GameManager.instancia.SairParaMenu();
        }
    }

    public void BotaoDespausar()
    {
        if (GameManager.instancia != null)
        {
            GameManager.instancia.AlternarPausa();
        }
    }
}