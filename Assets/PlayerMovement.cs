using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float MoveSpeed = 5f;
    [SerializeField] float jumpForce = 7f;
    [SerializeField] float boostModifier = 2f;
    [SerializeField] LayerMask groundLayer; // specify what is "ground"
    // [SerializeField] Transform groundCheck; // empty gameobject for ground detection
    // [SerializeField] float groundCheckRadius = 0.1f; // radius for groundcheck
    [SerializeField] PolygonCollider2D groundCheckCollider; // GroundCheck collider covering the player


    private Rigidbody2D rb;
    private Vector2 inputDirection = Vector2.zero;
    private bool isGrounded = false; // track if the player is grounded 
    private bool canDoubleJump = false; // track if player can double jump
    private int jumpCount = 0; // number of jumps done

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnMove(InputValue value)
    {
        Vector2 movementDir = value.Get<Vector2>();
        Debug.Log(movementDir);
        // rb.AddForce(movementDir * boostModifier, ForceMode2D.Impulse);

        inputDirection = movementDir;
    }

    private void Update()
    {
        // check if player is grounded before adding force - ground check logic 
        // isGrounded = Physics2D.OverlapPolygon(groundCheckCollider.bounds.center, groundCheckCollider.bounds.size, 0f, groundLayer);

        // Ground check using the PolygonCollider2D
        isGrounded = IsGrounded();

        // Debugging Ground Detection
        Debug.Log("Grounded: " + isGrounded);

        if (isGrounded)
        {
            //only apply if the player is on the ground
            // rb.AddForce(inputDirection * boostModifier);


           // jumpCount = 0; // reset jumpcount
           canDoubleJump = true; // allow to double jump after landing
        }

            // Apply continuous movement force based on player input
            rb.velocity = new Vector2(inputDirection.x * boostModifier, rb.velocity.y);
        
    }

    private bool IsGrounded()
    {
        // Create an array to store points from the PolygonCollider2D
        Vector2[] points = groundCheckCollider.points;
        Vector2 position = groundCheckCollider.transform.position;

        // Check for ground using each point in the collider
        foreach (var point in points)
        {
            // Transform the local point to world space
            Vector2 worldPoint = position + (Vector2)(groundCheckCollider.transform.TransformVector(point));
            // Check if this point is overlapping with the ground layer
            if (Physics2D.OverlapCircle(worldPoint, 0.1f, groundLayer)) // Adjust the radius as necessary
            {
                return true;
            }
        }

        return false; // No points were overlapping with the ground
    }


    private void OnJump(InputValue value)
    {
        // if the player is grounded, allow to jump - jump logic

        if (isGrounded)
        {
            Jump();
        }

        else if (canDoubleJump) // only allow double jump if available
        {
            Jump();
            canDoubleJump = false; // disable double jump after use
        }


    }

    private void Jump()
    {
        // apply upward force for jump
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        jumpCount++; // increment jump count
    }

    private void OnDrawGizmos()
    {
        // Visualize the ground check in the editor (draw wire spheres for each point)
        Gizmos.color = Color.red;
        foreach (var point in groundCheckCollider.points)
        {
            Vector2 worldPoint = (Vector2)groundCheckCollider.transform.position + (Vector2)(groundCheckCollider.transform.TransformVector(point));
            Gizmos.DrawWireSphere(worldPoint, 0.1f); // Adjust the radius as necessary
        }
    }

        private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("balls");
    }
}
