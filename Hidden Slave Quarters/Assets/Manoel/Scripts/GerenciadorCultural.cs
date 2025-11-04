using UnityEngine;
using System.Collections.Generic;

public class GerenciadorCultural : MonoBehaviour
{
    [System.Serializable]
    public class InformacaoCultural
    {
        public string id;
        public string titulo;
        [TextArea(3, 8)]
        public string descricao;
        public Sprite imagem;
        public AudioClip audioDescricao;
        public int pontosConhecimento = 15;
    }

    public static GerenciadorCultural Instancia;

    [Header("Configurações Culturais")]
    public List<InformacaoCultural> bancoInformacoes = new List<InformacaoCultural>();

    private Dictionary<string, InformacaoCultural> dicionarioInformacoes = new Dictionary<string, InformacaoCultural>();

    void Awake()
    {
        if (Instancia == null)
        {
            Instancia = this;
            DontDestroyOnLoad(gameObject);
            ConfigurarBancoDados();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void ConfigurarBancoDados()
    {
        dicionarioInformacoes.Clear();

        // Adicionar informações básicas se estiver vazio
        if (bancoInformacoes.Count == 0)
        {
            InformacaoCultural abdiasInfo = new InformacaoCultural
            {
                id = "abdias_nascimento",
                titulo = "Abdias Nascimento",
                descricao = "Abdias do Nascimento (1914-2011) foi um importante ativista dos direitos civis das populações negras, político, professor, artista plástico e escritor brasileiro. Fundou o Teatro Experimental do Negro e dedicou sua vida ao combate ao racismo.",
                pontosConhecimento = 25
            };
            bancoInformacoes.Add(abdiasInfo);

            InformacaoCultural quilomboInfo = new InformacaoCultural
            {
                id = "quilombos",
                titulo = "Quilombos - Territórios de Liberdade",
                descricao = "Os quilombos eram comunidades autônomas formadas por pessoas escravizadas que conseguiam fugir. Representavam resistência, organização e esperança de liberdade. O mais famoso foi o Quilombo dos Palmares, liderado por Zumbi.",
                pontosConhecimento = 20
            };
            bancoInformacoes.Add(quilomboInfo);

            InformacaoCultural capoeiraInfo = new InformacaoCultural
            {
                id = "capoeira",
                titulo = "Capoeira - Resistência Disfarçada",
                descricao = "A capoeira surgiu como forma de resistência dos escravizados no Brasil. Disfarçada de dança, era na verdade uma arte marcial que permitia a defesa e preparação para fugas. Era praticada nas senzalas como forma de preservar cultura e preparar o corpo para a liberdade.",
                pontosConhecimento = 15
            };
            bancoInformacoes.Add(capoeiraInfo);
        }

        // Popular o dicionário
        foreach (InformacaoCultural info in bancoInformacoes)
        {
            if (!string.IsNullOrEmpty(info.id) && !dicionarioInformacoes.ContainsKey(info.id))
            {
                dicionarioInformacoes.Add(info.id, info);
            }
        }

        Debug.Log($"Gerenciador Cultural inicializado com {dicionarioInformacoes.Count} informações.");
    }

    public void RevelarInformacaoCultural(string idInformacao)
    {
        if (dicionarioInformacoes.ContainsKey(idInformacao))
        {
            InformacaoCultural info = dicionarioInformacoes[idInformacao];

            // CORREÇÃO: Usar FindAnyObjectByType em vez do método obsoleto
            PersonagemController jogador = FindAnyObjectByType<PersonagemController>();
            if (jogador != null)
            {
                jogador.conhecimentoCultural += info.pontosConhecimento;
            }

            // Mostrar na UI
            if (UIManager.instancia != null)
            {
                string mensagem = $"<b>{info.titulo}</b>\n{info.descricao}\n\n+{info.pontosConhecimento} pontos de conhecimento!";
                UIManager.instancia.MostrarMensagem(mensagem);
                UIManager.instancia.AtualizarUI();
            }

            // Tocar áudio se disponível
            if (info.audioDescricao != null)
            {
                AudioSource audioSource = GetComponent<AudioSource>();
                if (audioSource == null)
                {
                    audioSource = gameObject.AddComponent<AudioSource>();
                }
                audioSource.PlayOneShot(info.audioDescricao);
            }

            Debug.Log($"Informação cultural revelada: {info.titulo}");
        }
        else
        {
            Debug.LogWarning($"Informação cultural não encontrada: {idInformacao}");
        }
    }

    public bool VerificarInformacaoExiste(string idInformacao)
    {
        return dicionarioInformacoes.ContainsKey(idInformacao);
    }

    public List<string> ObterTodosIds()
    {
        return new List<string>(dicionarioInformacoes.Keys);
    }

    public InformacaoCultural ObterInformacaoPorId(string id)
    {
        if (dicionarioInformacoes.ContainsKey(id))
        {
            return dicionarioInformacoes[id];
        }
        return null;
    }
}