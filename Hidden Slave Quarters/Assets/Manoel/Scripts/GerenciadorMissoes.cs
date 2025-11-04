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
            titulo = "Fuga da Senzala",
            descricao = "Encontre uma maneira de escapar da senzala observando os padroes dos guardas e aproveitando a tempestade.",
            objetivos = new List<ObjetivoMissao>
            {
                new ObjetivoMissao
                {
                    idObjetivo = "observar_guardas",
                    descricao = "Observe os padroes de movimento dos guardas",
                    triggerCompletacao = "GUARDAS_OBSERVADOS"
                },
                new ObjetivoMissao
                {
                    idObjetivo = "encontrar_ferramenta",
                    descricao = "Encontre uma ferramenta para ajudar na fuga",
                    triggerCompletacao = "FERRAMENTA_ENCONTRADA"
                },
                new ObjetivoMissao
                {
                    idObjetivo = "esperar_tempestade",
                    descricao = "Espere pela tempestade para cobrir o barulho",
                    triggerCompletacao = "TEMPESTADE_CHEGOU"
                },
                new ObjetivoMissao
                {
                    idObjetivo = "escapar_senzala",
                    descricao = "Fuja da senzala enquanto os guardas estao distraidos",
                    triggerCompletacao = "SENZALA_ESCAPADA"
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