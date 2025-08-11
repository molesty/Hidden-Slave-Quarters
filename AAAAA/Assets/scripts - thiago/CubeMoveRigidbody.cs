using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CubeMoveRigidbody : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 10f;

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal"); // A/D - esquerda/direita (X)
        float v = Input.GetAxisRaw("Vertical");   // W/S - frente/trás (Z)

        // Teclas para cima e baixo (E sobe, Q desce)
        float upDown = 0f;
        if (Input.GetKey(KeyCode.E)) upDown = 1f;
        else if (Input.GetKey(KeyCode.Q)) upDown = -1f;

        Vector3 move = new Vector3(h, upDown, v).normalized;

        Vector3 velocity = move * speed;
        rb.linearVelocity = velocity;

        if (move.sqrMagnitude > 0.001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(new Vector3(h, 0f, v), Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.fixedDeltaTime);
        }
    }
}
