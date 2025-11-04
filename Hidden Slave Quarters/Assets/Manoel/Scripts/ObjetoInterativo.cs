using UnityEngine;

public class ObjetoInterativo : MonoBehaviour
{
    public enum TipoInteracao
    {
        Pista,
        Ferramenta,
        Simbolo,
        AreaEscape
    }

    [Header("Configuracoes da Interacao")]
    public TipoInteracao tipo;
    public string triggerMissao;
    public string mensagemInteracao;
    public int pontosConhecimento = 10;

    [Header("Efeitos Visuais")]
    public ParticleSystem particulas;
    public GameObject luzDestaque;
    public AudioClip somInteracao;

    private bool jaInteragido = false;

    void Start()
    {
        if (luzDestaque != null)
            luzDestaque.SetActive(false);
    }

    public void Interagir()
    {
        if (jaInteragido) return;

        jaInteragido = true;

        Debug.Log("Interagindo com: " + gameObject.name + " - Trigger: " + triggerMissao);

        if (!string.IsNullOrEmpty(triggerMissao))
        {
            GerenciadorMissoes.instancia.CompletarObjetivo(triggerMissao);
        }

        if (pontosConhecimento > 0)
        {
            PersonagemController jogador = FindAnyObjectByType<PersonagemController>();
            if (jogador != null)
            {
                jogador.AdicionarConhecimento(pontosConhecimento);
            }
        }

        if (!string.IsNullOrEmpty(mensagemInteracao))
        {
            if (UIManager.instancia != null)
                UIManager.instancia.MostrarMensagem(mensagemInteracao);
        }

        if (particulas != null)
            particulas.Play();

        if (luzDestaque != null)
            luzDestaque.SetActive(true);

        if (somInteracao != null)
        {
            AudioSource.PlayClipAtPoint(somInteracao, transform.position);
        }

        if (tipo == TipoInteracao.Ferramenta || tipo == TipoInteracao.Pista)
        {
            GetComponent<Collider2D>().enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !jaInteragido)
        {
            if (luzDestaque != null)
                luzDestaque.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (luzDestaque != null)
                luzDestaque.SetActive(false);
        }
    }
}