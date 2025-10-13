using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public float speed = 6f;
    public float jumpForce = 10f;
    Rigidbody2D rb;
    Animator animator;
    bool controlEnabled = true;

    public static event Action OnControlEnabled;
    public static event Action OnControlDisabled;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        if (!GameStateManager.I.cutsceneSeen) DisableControl(); // begin disabled
    }

    void Update()
    {
        if (!controlEnabled) return;
        float h = Input.GetAxisRaw("Horizontal");
        Move(h);
        if (Input.GetButtonDown("Jump")) Jump();
        if (Input.GetButtonDown("Fire1")) Attack();
    }

    void Move(float h)
    {
        Vector2 vel = rb.linearVelocity;
        vel.x = h * speed;
        rb.linearVelocity = vel;
        animator.SetFloat("Speed", Mathf.Abs(h));
        if (h != 0) transform.localScale = new Vector3(Mathf.Sign(h), 1, 1);
    }

    void Jump()
    {
        // implement ground check properly later
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        animator.SetTrigger("Jump");
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
    }

    public void EnableControl()
    {
        controlEnabled = true;
        OnControlEnabled?.Invoke();
    }
    public void DisableControl()
    {
        controlEnabled = false;
        OnControlDisabled?.Invoke();
        rb.linearVelocity = Vector2.zero;
    }
}
