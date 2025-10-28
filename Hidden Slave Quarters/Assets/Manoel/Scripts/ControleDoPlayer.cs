using UnityEngine;

public class ControleDoPlayer : MonoBehaviour
{
    public float speed = 3f;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        anim.SetFloat("Speed", moveInput.sqrMagnitude);

        if (moveInput.x > 0)
            sr.flipX = false;
        else if (moveInput.x < 0)
            sr.flipX = true;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput.normalized * speed * Time.fixedDeltaTime);
    }
}
