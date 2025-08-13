using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocidade = 5f;
    public float forcaPulo = 5f;
    private Rigidbody2D rig;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Mover();
        if (Input.GetButtonDown("Jump"))
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
}
