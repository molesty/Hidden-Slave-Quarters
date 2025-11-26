using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // velocidade de movimento
    public Rigidbody2D rb;       // referência ao Rigidbody2D
    Vector2 movement;            // guarda a direção do movimento

    void Update()
    {
        // Pega o input do teclado (horizontal e vertical)
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        // Move o player baseado no input
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}