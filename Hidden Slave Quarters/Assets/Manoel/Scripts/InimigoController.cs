using UnityEngine;
using System.Collections;

public class InimigoController : MonoBehaviour
{
    [Header("Configuracoes do Guarda")]
    public float velocidadePatrulha = 2f;
    public float velocidadePerseguicao = 4f;
    public float tempoEsquecerJogador = 5f;
    public float distanciaParada = 0.3f;

    [Header("Pontos de Patrulha")]
    public Transform[] pontosPatrulha;

    private Transform jogador;
    private bool jogadorDetectado = false;
    private Vector3 ultimaPosicaoJogador;
    private float tempoUltimaDetecao;
    private int pontoAtual = 0;
    private bool estaPatrulhando = false;

    void Start()
    {
        jogador = GameObject.FindGameObjectWithTag("Player").transform;

        if (pontosPatrulha != null && pontosPatrulha.Length > 0)
        {
            IniciarPatrulha();
        }
    }

    void Update()
    {
        if (jogadorDetectado)
        {
            PerseguirJogador();

            if (Time.time - tempoUltimaDetecao > tempoEsquecerJogador)
            {
                jogadorDetectado = false;
                IniciarPatrulha();
            }
        }
    }

    void IniciarPatrulha()
    {
        if (!estaPatrulhando && pontosPatrulha.Length > 0)
        {
            estaPatrulhando = true;
            StartCoroutine(RotinaPatrulha());
        }
    }

    public void DetectarJogador(Vector3 posicaoJogador)
    {
        jogadorDetectado = true;
        estaPatrulhando = false;
        ultimaPosicaoJogador = posicaoJogador;
        tempoUltimaDetecao = Time.time;

        StopAllCoroutines();
    }

    void PerseguirJogador()
    {
        Vector3 direcao = (ultimaPosicaoJogador - transform.position).normalized;
        transform.position += direcao * velocidadePerseguicao * Time.deltaTime;

        if (jogador != null)
        {
            RaycastHit2D hit = Physics2D.Raycast(
                transform.position,
                jogador.position - transform.position,
                10f);

            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                ultimaPosicaoJogador = jogador.position;
                tempoUltimaDetecao = Time.time;
            }
        }
    }

    System.Collections.IEnumerator RotinaPatrulha()
    {
        while (!jogadorDetectado && pontosPatrulha.Length > 0)
        {
            if (pontosPatrulha[pontoAtual] == null)
            {
                pontoAtual = (pontoAtual + 1) % pontosPatrulha.Length;
                yield return new WaitForSeconds(1f);
                continue;
            }

            Vector3 alvo = pontosPatrulha[pontoAtual].position;

            while (Vector3.Distance(transform.position, alvo) > distanciaParada && !jogadorDetectado)
            {
                if (pontosPatrulha[pontoAtual] == null) break;

                Vector3 direcao = (alvo - transform.position).normalized;
                transform.position += direcao * velocidadePatrulha * Time.deltaTime;

                yield return null;
            }

            if (jogadorDetectado) break;

            yield return new WaitForSeconds(2f);

            pontoAtual = (pontoAtual + 1) % pontosPatrulha.Length;
        }

        estaPatrulhando = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (GameManager.instancia != null)
            {
                GameManager.instancia.JogadorCapturado();
            }
        }
    }
}