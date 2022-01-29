using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpSpeed;
    [SerializeField]
    private float maxJumpTime;
    [SerializeField]
    private Rigidbody2D rigidbody2D;
    [SerializeField]
    private float groundCheckDistance;
    [SerializeField]
    private SpriteRenderer renderer;
    [SerializeField]
    private Transform groundTestOrigin;
    [SerializeField]
    private LayerMask groundLayers;
    [SerializeField]
    private Vector3 groundCheckSize;

    private Vector2 moveVector;
    private bool jumping;
    private float currentJumpDuration;
    private bool grounded;

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Get horizontal movement
        float xMove = Input.GetAxis("Horizontal");
        moveVector = new Vector3(xMove, 0.0f);

        // Jump
        if (Input.GetButtonDown("Jump") && grounded)
        {
            jumping = true;
        }

        // Check jump duration
        if (jumping)
        {
            currentJumpDuration += Time.deltaTime;

            if (currentJumpDuration >= maxJumpTime)
            {
                jumping = false;
                currentJumpDuration = 0.0f;
            }
        }
    }

    private void FixedUpdate()
    {
        // Apply movement in physics, keeping vertical speed
        Vector2 moveVelocity = moveVector * moveSpeed * Time.deltaTime;
        Vector3 desiredVelocity = new Vector2(0.0f, rigidbody2D.velocity.y) + moveVelocity;
        rigidbody2D.velocity = desiredVelocity;

        // Jump physics, keep horizontal speed
        if (jumping)
        {
            Vector2 jumpVelocity = new Vector3(0.0f, jumpSpeed, 0.0f) * Time.deltaTime;
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0.0f) + jumpVelocity;
        }

        // Ground test
        RaycastHit2D hit = Physics2D.BoxCast(groundTestOrigin.position, groundCheckSize, 0.0f, -Vector2.up, groundCheckDistance, groundLayers);
        if (hit.collider != null)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(groundTestOrigin.position, groundTestOrigin.position - Vector3.up * groundCheckDistance);
    }

    private void LateUpdate()
    {
        // Ground debug
        if (grounded)
        {
            renderer.color = Color.blue;
        }
        else
        {
            renderer.color = Color.red;
        }
    }
}
