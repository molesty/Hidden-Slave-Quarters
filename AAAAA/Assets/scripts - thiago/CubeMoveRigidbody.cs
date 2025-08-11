using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CubeMoveRigidbody : MonoBehaviour
{
    public float speed = 5f;           // velocidade de movimento
    public float rotationSpeed = 10f;  // opcional: faz o cubo "virar" na direção do movimento

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        // Para controle mais direto, evitar rotações indesejadas
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        // Entrada WASD / setas
        float h = Input.GetAxisRaw("Horizontal"); // A/D, esquerda/direita
        float v = Input.GetAxisRaw("Vertical");   // W/S, frente/trás

        Vector3 move = new Vector3(h, 0f, v).normalized;

        // aplica velocidade no Rigidbody
        Vector3 velocity = move * speed;
        // preserva componente Y (gravidade)
        velocity.y = rb.linearVelocity.y;
        rb.linearVelocity = velocity;

        // opcional: rotaciona suavemente para direção do movimento
        if (move.sqrMagnitude > 0.001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(move, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.fixedDeltaTime);
        }
    }
}

