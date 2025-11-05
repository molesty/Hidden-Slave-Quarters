using UnityEngine;
using System.Collections;

public class PersonagemController : MonoBehaviour
{
    [Header("Configuracoes Point'n'Click")]
    public float velocidadeMovimento = 3f;
    public float distanciaParada = 0.1f;

    [Header("Estado do Personagem")]
    public bool estaEscondido = false;
    public bool estaObservando = false;
    public bool estaAndando = false;
    public int conhecimentoCultural = 0;

    private Vector3 destino;
    private Animator animator;
    private bool viradoDireita = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        destino = transform.position;
    }

    void Update()
    {
        if (GameManager.instancia != null && GameManager.instancia.estadoAtual != GameManager.EstadoJogo.Jogando)
            return;

        ProcessarClick();
        MoverParaDestino();
        HandleInteraction();
        AtualizarAnimacao();
        VerificarDetecao();
    }

    void ProcessarClick()
    {
        if (Input.GetMouseButtonDown(0))
        { 
            Vector3 posicaoMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            posicaoMouse.z = 0;

            RaycastHit2D hit = Physics2D.Raycast(posicaoMouse, Vector2.zero);
            if (hit.collider != null)
            {
                ItemColetavel item = hit.collider.GetComponent<ItemColetavel>();
                if (item != null)
                {
                    item.Coletar();
                    return;
                }

                NPCController npc = hit.collider.GetComponent<NPCController>();
                if (npc != null)
                {
                    destino = hit.transform.position;
                    StartCoroutine(MoverEInteragir(npc));
                    return;
                }

                ObjetoInterativo obj = hit.collider.GetComponent<ObjetoInterativo>();
                if (obj != null)
                {
                    destino = hit.transform.position;
                    StartCoroutine(MoverEInteragir(obj));
                    return;
                }

                PistaAmbiente pista = hit.collider.GetComponent<PistaAmbiente>();
                if (pista != null)
                {
                    destino = hit.transform.position;
                    StartCoroutine(MoverEInteragir(pista));
                    return;
                }
            }

            destino = posicaoMouse;
        }
    }

    void MoverParaDestino()
    {
        float distancia = Vector3.Distance(transform.position, destino);

        if (distancia > distanciaParada)
        {
            estaAndando = true;
            Vector3 direcao = (destino - transform.position).normalized;
            transform.position += direcao * velocidadeMovimento * Time.deltaTime;

            if (direcao.x > 0 && !viradoDireita)
            {
                VirarPersonagem();
            }
            else if (direcao.x < 0 && viradoDireita)
            {
                VirarPersonagem();
            }
        }
        else
        {
            estaAndando = false;
        }
    }

    void VirarPersonagem()
    {
        viradoDireita = !viradoDireita;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    IEnumerator MoverEInteragir(Component componente)
    {
        while (Vector3.Distance(transform.position, destino) > distanciaParada)
        {
            yield return null;
        }
        if (componente is ItemColetavel item)
        {
            item.Coletar();
        }
        else if (componente is NPCController npc)
        {
            npc.Interagir();
        }
        else if (componente is ObjetoInterativo obj)
        {
            obj.Interagir();
        }
        else if (componente is PistaAmbiente pista)
        {
            pista.RevelarPista();
        }
    }

    void HandleInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            IniciarObservacao();
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
        Collider2D[] objetosProximos = Physics2D.OverlapCircleAll(transform.position, 2f);

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
        if (estaAndando && !estaEscondido)
        {
            Collider2D[] guardasProximos = Physics2D.OverlapCircleAll(transform.position, 4f);

            foreach (Collider2D colisor in guardasProximos)
            {
                InimigoController guarda = colisor.GetComponent<InimigoController>();
                if (guarda != null)
                {
                    guarda.DetectarJogador(transform.position);
                }
            }
        }
    }

    void AtualizarAnimacao()
    {
        if (animator != null && animator.runtimeAnimatorController != null)
        {
            animator.SetBool("Andando", estaAndando);
            animator.SetBool("Escondido", estaEscondido);
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(destino, 0.3f);
        Gizmos.DrawLine(transform.position, destino);
    }
}