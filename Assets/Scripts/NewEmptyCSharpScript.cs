using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;          // Horizontal speed
    public float jumpForce = 7f;          // Jump strength

    [Header("Ground Check Settings")]
    public Transform groundCheck;         // Empty object under player
    public float checkRadius = 0.2f;      // Radius of overlap check
    public LayerMask groundLayer;         // Assign your "Ground" layer here

    [Header("References")]
    private Rigidbody2D rb;
    private Animator anim;

    // Internal variables
    private float moveInput;
    private bool isGrounded;
    private bool facingRight = true;

    void Start()
    {
        // Get Rigidbody2D and Animator from Player
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (rb == null)
            Debug.LogError("No Rigidbody2D found on player!");

        if (anim == null)
            Debug.LogError("No Animator found on player!");
    }

    void Update()
    {
        // --- Ground Check ---
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        // Debugging
        Debug.Log($"Grounded: {isGrounded} | Velocity: {rb.velocity}");

        // --- Movement Input ---
        moveInput = Input.GetAxisRaw("Horizontal");  // A/D or Left/Right Arrow keys

        // --- Jump Logic ---
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            Debug.Log("Jump Triggered!");
        }

        // --- Flip Player Sprite ---
        if (facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }

        // --- Update Animator ---
        UpdateAnimations();
    }

    void FixedUpdate()
    {
        // Horizontal movement applied in FixedUpdate
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    private void UpdateAnimations()
    {
        // Set speed parameter for walk/run blend
        anim.SetFloat("Speed", Mathf.Abs(moveInput));

        // Grounded parameter
        anim.SetBool("isGrounded", isGrounded);

        // Vertical velocity for jump/fall blend
        anim.SetFloat("VerticalVelocity", rb.velocity.y);
    }

    private void OnDrawGizmosSelected()
    {
        // Draw ground check circle in Scene view
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
        }
    }
}