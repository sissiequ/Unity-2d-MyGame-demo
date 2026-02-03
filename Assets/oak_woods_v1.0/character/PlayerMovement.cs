using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector2 spawnPoint;

    public float moveSpeed = 5f;
    public float jumpForce = 12f;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;

    float moveInput;
    bool isGrounded;
    bool jumpRequested;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        spawnPoint = transform.position;
    }

    void ResetPlayer()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;  // Avoid flying after reset

        rb.position = spawnPoint;

        isGrounded = false;
        jumpRequested = false;

        animator.Play("Idle", 0, 0f);
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if (moveInput > 0) sr.flipX = false;
        else if (moveInput < 0) sr.flipX = true;

        animator.SetFloat("speed", Mathf.Abs(moveInput));
        animator.SetBool("isGrounded", isGrounded);

        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            jumpRequested = true;
        }

        // R: restart
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetPlayer();
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (jumpRequested)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpRequested = false;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ground")) return;

        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                isGrounded = true;
                return;
            }
        }

        isGrounded = false;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

   
}
