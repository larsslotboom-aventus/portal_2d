using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the player
    public float jumpHeight = 2f; // Desired jump height
    public Vector2 boxSize = new Vector2(0.5f, 0.1f); // Size of the box for ground check
    public LayerMask groundLayer; // Layer that represents the ground
    public float groundCheckOffset = 0.1f; // Offset for the ground check

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider; // Reference to the player's BoxCollider2D
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>(); // Get the BoxCollider2D component
    }

    void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    private void Jump()
    {
        // Perform a BoxCast using the player's collider dimensions with an offset
        Vector2 boxCastPosition = (Vector2)transform.position - new Vector2(0, groundCheckOffset);
        isGrounded = Physics2D.BoxCast(boxCastPosition, boxSize, 0f, Vector2.down, 0.1f, groundLayer);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            // Calculate the jump force needed to reach the desired jump height
            float jumpForce = Mathf.Sqrt(2 * Mathf.Abs(Physics2D.gravity.y) * jumpHeight);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    private void OnDrawGizmos()
    {
        // Draw the box for visual debugging
        Gizmos.color = Color.red;
        Vector2 boxCastPosition = (Vector2)transform.position - new Vector2(0, groundCheckOffset);
        Gizmos.DrawWireCube(boxCastPosition, boxSize);
    }
}