using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CubeTopDownRigidbody2D : MonoBehaviour
{
    public float speed = 5f;
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true; 
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector2 move = new Vector2(h, v);
        if (move.sqrMagnitude > 1f) move = move.normalized;

        rb.linearVelocity = move * speed;
    }
}
