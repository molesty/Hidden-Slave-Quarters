using UnityEngine;
using System.Collections.Generic;

public class GerenciadorMissoes : MonoBehaviour
{
    [System.Serializable]
    public class Missao
    {
        public string idMissao;
        public string titulo;
        [TextArea(3, 5)]
        public string descricao;
        public bool isActive = false;
        public bool isCompleted = false;
        public List<ObjetivoMissao> objetivos;
    }

    [System.Serializable]
    public class ObjetivoMissao
    {
        public string idObjetivo;
        [TextArea(2, 3)]
        public string descricao;
        public bool isCompleted = false;
        public string triggerCompletacao;
    }

    public static GerenciadorMissoes instancia;

    [Header("Lista de Missoes")]
    public List<Missao> todasMissoes = new List<Missao>();

    private Missao missaoAtual;
    private Dictionary<string, Missao> dicionarioMissoes = new Dictionary<string, Missao>();

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
            InicializarMissoes();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void InicializarMissoes()
    {
        Missao fugaSenzala = new Missao
        {
            idMissao = "fuga_senzala",
            titulo = "Ferramenta da Liberdade",
            descricao = "Encontre a chave velha e peca ao ferreiro para fazer uma copia",
            objetivos = new List<ObjetivoMissao>
    {
        new ObjetivoMissao
        {
            idObjetivo = "encontrar_chave",
            descricao = "Encontre a chave velha na senzala",
            triggerCompletacao = "COLETOU_CHAVE_VELHA"
        },
        new ObjetivoMissao
        {
            idObjetivo = "levar_ferreiro",
            descricao = "Leve a chave ao ferreiro",
            triggerCompletacao = "ENTREGUE_CHAVE_VELHA_PARA_FERREIRO"
        },
        new ObjetivoMissao
        {
            idObjetivo = "pegar_copia",
            descricao = "Pegue a copia da chave",
            triggerCompletacao = "COLETOU_CHAVE_COPIA"
        },
        new ObjetivoMissao
        {
            idObjetivo = "usar_fechadura",
            descricao = "Use a chave copia na fechadura",
            triggerCompletacao = "USOU_CHAVE_COPIA_EM_FECHADURA"
        }
    }
        };

        Missao caminhoQuilombo = new Missao
        {
            idMissao = "caminho_quilombo",
            titulo = "Caminho para o Quilombo",
            descricao = "Siga os simbolos secretos na floresta para encontrar o quilombo escondido.",
            objetivos = new List<ObjetivoMissao>
            {
                new ObjetivoMissao
                {
                    idObjetivo = "encontrar_primeiro_simbolo",
                    descricao = "Encontre o primeiro simbolo na floresta",
                    triggerCompletacao = "SIMBOLO_1_ENCONTRADO"
                },
                new ObjetivoMissao
                {
                    idObjetivo = "seguir_trilha",
                    descricao = "Siga a trilha de simbolos",
                    triggerCompletacao = "TRILHA_SEGUIDA"
                },
                new ObjetivoMissao
                {
                    idObjetivo = "encontrar_quilombo",
                    descricao = "Encontre a entrada do quilombo secreto",
                    triggerCompletacao = "QUILOMBO_ENCONTRADO"
                }
            }
        };

        todasMissoes.Add(fugaSenzala);
        todasMissoes.Add(caminhoQuilombo);

        foreach (Missao missao in todasMissoes)
        {
            dicionarioMissoes[missao.idMissao] = missao;
        }

        Debug.Log("Sistema de missoes inicializado com " + todasMissoes.Count + " missoes");
    }

    void Start()
    {
        IniciarMissao("fuga_senzala");
    }

    public void IniciarMissao(string idMissao)
    {
        if (dicionarioMissoes.ContainsKey(idMissao))
        {
            missaoAtual = dicionarioMissoes[idMissao];
            missaoAtual.isActive = true;

            Debug.Log("Missao iniciada: " + missaoAtual.titulo);

            if (UIManager.instancia != null)
            {
                UIManager.instancia.MostrarMissao(missaoAtual);
            }

            if (UIManager.instancia != null)
                UIManager.instancia.MostrarMensagem("Nova Missao: " + missaoAtual.titulo);
        }
        else
        {
            Debug.LogError("Missao nao encontrada: " + idMissao);
        }
    }

    public void CompletarObjetivo(string trigger)
    {
        if (missaoAtual == null || !missaoAtual.isActive)
        {
            Debug.LogWarning("Nenhuma missao ativa para completar objetivo");
            return;
        }

        foreach (ObjetivoMissao objetivo in missaoAtual.objetivos)
        {
            if (objetivo.triggerCompletacao == trigger && !objetivo.isCompleted)
            {
                objetivo.isCompleted = true;

                Debug.Log("Objetivo completado: " + objetivo.descricao);

                if (UIManager.instancia != null)
                {
                    UIManager.instancia.MostrarMensagem("Objetivo Concluido: " + objetivo.descricao);
                    UIManager.instancia.AtualizarUI();
                }

                VerificarMissaoCompleta();
                return;
            }
        }

        Debug.LogWarning("Trigger de objetivo nao encontrado: " + trigger);
    }

    void VerificarMissaoCompleta()
    {
        if (missaoAtual.objetivos.TrueForAll(obj => obj.isCompleted))
        {
            missaoAtual.isCompleted = true;
            missaoAtual.isActive = false;

            Debug.Log("MISSÃO COMPLETA: " + missaoAtual.titulo);

            if (UIManager.instancia != null)
            {
                UIManager.instancia.MostrarMensagem("MISSÃO COMPLETA: " + missaoAtual.titulo);
            }

            DeterminarProximaMissao();
        }
    }

    void DeterminarProximaMissao()
    {
        switch (missaoAtual.idMissao)
        {
            case "fuga_senzala":
                Invoke("IniciarMissaoQuilombo", 3f);
                break;
            case "caminho_quilombo":
                Invoke("FinalizarJogo", 5f);
                break;
        }
    }

    void IniciarMissaoQuilombo()
    {
        IniciarMissao("caminho_quilombo");
    }

    void FinalizarJogo()
    {
        Debug.Log("TODAS AS MISSOES COMPLETAS - FIM DO JOGO");
        if (UIManager.instancia != null)
        {
            UIManager.instancia.MostrarMensagem("PARABENS! Voce completou todas as missoes!");
        }
    }

    public Missao GetMissaoAtual()
    {
        return missaoAtual;
    }
}