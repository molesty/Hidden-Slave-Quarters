
using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public enum State { Idle, Patrol, Chase, Attack, Hurt, Dead }
    public State state = State.Patrol;

    [Header("References")]
    public Transform player;
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    [Header("Movement")]
    public float speed = 2.5f;
    public Transform[] patrolPoints; // opcional: se vazio, fica parado
    private int currentPatrolIndex = 0;

    [Header("Detection & Combat")]
    public float detectionRadius = 6f; // quando "v�" o player
    public float attackRange = 1.2f; // alcance de ataque
    public LayerMask obstacleMask; // para verificar linha de vis�o (opcional)

    [Header("Stats")]
    public int maxHealth = 50;
    private int currentHealth;
    public int damage = 10;
    public float attackCooldown = 1.2f;
    private bool canAttack = true;

    [Header("Misc")]
    public Transform attackPoint; // local onde o ataque "acontece"
    public float attackRadius = 0.6f; // area do ataque (OverlapCircle)
    public LayerMask playerLayer;

    // Internals
    private Vector2 velocity;

    void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        if (animator == null) animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (state == State.Dead) return;

        float dist = Vector2.Distance(transform.position, player.position);
        bool canSee = CanSeePlayer();

        // Estado b�sico de decis�o
        if (dist <= attackRange && canSee)
        {
            state = State.Attack;
        }
        else if (dist <= detectionRadius && canSee)
        {
            state = State.Chase;
        }
        else
        {
            if (patrolPoints != null && patrolPoints.Length > 0) state = State.Patrol;
            else state = State.Idle;
        }

        HandleState();
    }

    void HandleState()
    {
        switch (state)
        {
            case State.Idle:
                velocity = Vector2.zero;
                animator.SetFloat("Speed", 0f);
                break;

            case State.Patrol:
                if (patrolPoints.Length == 0) { state = State.Idle; break; }
                Vector2 target = patrolPoints[currentPatrolIndex].position;
                MoveTowards(target);
                animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
                if (Vector2.Distance(transform.position, target) < 0.2f)
                {
                    currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
                }
                break;

            case State.Chase:
                MoveTowards(player.position);
                animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
                break;

            case State.Attack:
                rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
                animator.SetFloat("Speed", 0f);
                if (canAttack) StartCoroutine(DoAttack());
                break;

            case State.Hurt:
                rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
                animator.SetFloat("Speed", 0f);
                break;

            case State.Dead:
                rb.linearVelocity = Vector2.zero;
                animator.SetBool("isDead", true);
                break;
        }
    }

    void MoveTowards(Vector2 targetPos)
    {
        Vector2 dir = (targetPos - (Vector2)transform.position).normalized;
        rb.linearVelocity = new Vector2(dir.x * speed, rb.linearVelocity.y);
        FlipSprite(dir.x);
    }

    void FlipSprite(float dirX)
    {
        // Use flipX para evitar perda de qualidade no sprite ao virar.
        if (dirX > 0.01f) spriteRenderer.flipX = false; // assumir sprites virados para esquerda por padr�o, ajuste se necess�rio
        else if (dirX < -0.01f) spriteRenderer.flipX = true;
    }

    IEnumerator DoAttack()
    {
        canAttack = false;
        animator.SetTrigger("Attack");
        // Aguarde o timing do ataque � ajuste conforme sua anima��o (ex: 0.4s at� a batida)
        yield return new WaitForSeconds(0.3f);
        // efeito do ataque: detectar player na �rea
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, attackRadius, playerLayer);
        if (hit)
        {
            // assume que o player tem um m�todo TakeDamage(int)
            hit.SendMessageUpwards("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
        }
        // espera o cooldown
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    public void TakeDamage(int amount)
    {
        if (state == State.Dead) return;
        currentHealth -= amount;
        animator.SetTrigger("Hurt");
        state = currentHealth > 0 ? State.Hurt : State.Dead;
        if (currentHealth <= 0) Die();
        else StartCoroutine(RecoverFromHurt());
    }

    IEnumerator RecoverFromHurt()
    {
        // tempo de atordoamento curto
        yield return new WaitForSeconds(0.35f);
        state = State.Patrol; // ou Idle � voc� pode alterar a l�gica para voltar ao Chase se o player estiver perto
    }

    void Die()
    {
        state = State.Dead;
        animator.SetBool("isDead", true);
        // desabilitar colisores/IA ap�s um tempo
        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;
        // opcional: destruir ap�s X segundos
        Destroy(gameObject, 3f);
    }

    bool CanSeePlayer()
    {
        if (player == null) return false;
        Vector2 dir = player.position - transform.position;
        if (Mathf.Abs(dir.y) > 2.5f) return false; // evita ver players muito acima/abaixo (ajuste conforme cena)

        if (Physics2D.Raycast(transform.position, dir.normalized, detectionRadius, obstacleMask))
        {
            // atingiu um obst�culo antes do player
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir.normalized, detectionRadius, obstacleMask | (1 << player.gameObject.layer));
            if (hit.collider != null && hit.collider.transform == player) return true;
            return false;
        }
        else
        {
            // sem obst�culos, apenas checar dist�ncia
            return Vector2.Distance(transform.position, player.position) <= detectionRadius;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        }
    }
}
