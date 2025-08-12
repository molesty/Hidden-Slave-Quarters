using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocidade = 5f;
    public float forcaPulo = 5f;
    private Rigidbody2D rig;
    private bool noChao;

 
    public LayerMask layerChao;
    public Transform verificadorChao;
    public float raioVerificacao = 0.2f;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Mover();

        noChao = Physics2D.OverlapCircle(verificadorChao.position, raioVerificacao, layerChao);


        if (Input.GetButtonDown("Jump") && noChao)
        {
            Pular();
        }
    }

    void Mover()
    {
        float horizontal = Input.GetAxis("Horizontal");
        Vector2 movimento = new Vector2(horizontal * velocidade, rig.linearVelocity.y);
        rig.linearVelocity = movimento;
    }

    void Pular()
    {
        rig.AddForce(Vector2.up * forcaPulo, ForceMode2D.Impulse);
    }


    void OnDrawGizmosSelected()
    {
        if (verificadorChao != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(verificadorChao.position, raioVerificacao);
        }
    }
}
