using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Velocidade de movimento

    void Update()
    {
        // Captura o input de movimento
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        // Cria um vetor de movimento
        Vector2 movement = new Vector2(moveX, moveY);

        // Move o personagem (2D)
        transform.Translate(movement * speed * Time.deltaTime);
    }
}