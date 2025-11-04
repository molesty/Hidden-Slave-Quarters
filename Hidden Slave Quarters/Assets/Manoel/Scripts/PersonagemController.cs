using UnityEngine;
using System.Collections;

public class PersonagemController : MonoBehaviour
{
    [Header("Configuracoes de Movimento")]
    public float velocidadeNormal = 3f;
    public float velocidadeFurtiva = 1.5f;
    public float distanciaDetecao = 5f;

    [Header("Estado do Personagem")]
    public bool estaEscondido = false;
    public bool estaObservando = false;
    public int conhecimentoCultural = 0;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movimento;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (GameManager.instancia != null && GameManager.instancia.estadoAtual != GameManager.EstadoJogo.Jogando)
            return;

        ProcessarInput();
        HandleInteraction();
        //AtualizarAnimacao();
        VerificarDetecao();
    }

    void ProcessarInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        movimento = new Vector2(moveX, moveY).normalized;

        float velocidadeAtual = Input.GetKey(KeyCode.LeftShift) ?
                              velocidadeFurtiva : velocidadeNormal;

        if (rb != null)
        {
            rb.linearVelocity = movimento * velocidadeAtual;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            IniciarObservacao();
        }
    }

    void HandleInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TentarInteragir();
        }
    }

    void TentarInteragir()
    {
        Collider2D[] interactables = Physics2D.OverlapCircleAll(transform.position, 1.5f);

        foreach (Collider2D collider in interactables)
        {
            ObjetoInterativo interactable = collider.GetComponent<ObjetoInterativo>();
            if (interactable != null)
            {
                interactable.Interagir();
                return;
            }

            PistaAmbiente pista = collider.GetComponent<PistaAmbiente>();
            if (pista != null)
            {
                pista.RevelarPista();
                return;
            }
        }
    }

    void IniciarObservacao()
    {
        StartCoroutine(ObservarAmbiente());
    }

    IEnumerator ObservarAmbiente()
    {
        estaObservando = true;

        RevelarInformacoesAmbiente();

        yield return new WaitForSeconds(2f);

        estaObservando = false;
    }

    void RevelarInformacoesAmbiente()
    {
        Collider2D[] objetosProximos = Physics2D.OverlapCircleAll(transform.position, 3f);

        foreach (Collider2D colisor in objetosProximos)
        {
            PistaAmbiente pista = colisor.GetComponent<PistaAmbiente>();
            if (pista != null)
            {
                pista.RevelarPista();
                conhecimentoCultural += pista.valorCultural;

                if (UIManager.instancia != null)
                {
                    UIManager.instancia.AtualizarUI();
                }
            }
        }
    }

    void VerificarDetecao()
    {
        Collider2D[] guardasProximos = Physics2D.OverlapCircleAll(transform.position, distanciaDetecao);

        foreach (Collider2D colisor in guardasProximos)
        {
            InimigoController guarda = colisor.GetComponent<InimigoController>();
            if (guarda != null && !estaEscondido)
            {
                guarda.DetectarJogador(transform.position);
            }
        }
    }

    public void AdicionarConhecimento(int pontos)
    {
        conhecimentoCultural += pontos;
        if (UIManager.instancia != null)
        {
            UIManager.instancia.AtualizarUI();
        }
    }
}