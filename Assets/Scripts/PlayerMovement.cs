using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private PlayerMoveData humanModeData;
    [SerializeField]
    private PlayerMoveData robotModeData;

    [SerializeField]
    private LayerMask groundLayers;    
    [SerializeField]
    private MovementMode movementMode;

    private Rigidbody2D playerRigidbody;
    private Vector2 moveVector;
    private bool jumping;
    private float currentJumpDuration;
    private bool grounded;
    private PlayerMoveData modeData;

    private void Start()
    {
        modeData = humanModeData;
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Check mode
        if (movementMode.robotMode)
        {
            modeData = robotModeData;
        }
        else
        {
            modeData = humanModeData;
        }

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

            if (currentJumpDuration >= modeData.maxJumpTime)
            {
                jumping = false;
                currentJumpDuration = 0.0f;
            }
        }
    }

    private void FixedUpdate()
    {
        // Apply movement in physics, keeping vertical speed
        Vector2 moveVelocity = moveVector * modeData.moveSpeed * Time.deltaTime;
        Vector3 desiredVelocity = new Vector2(0.0f, playerRigidbody.velocity.y) + moveVelocity;
        playerRigidbody.velocity = desiredVelocity;

        // Jump physics, keep horizontal speed
        if (jumping)
        {
            Vector2 jumpVelocity = new Vector3(0.0f, modeData.jumpSpeed, 0.0f) * Time.deltaTime;
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0.0f) + jumpVelocity;
        }

        // Ground test
        RaycastHit2D hit = Physics2D.BoxCast(
            modeData.groundTestOrigin.position, 
            modeData.groundCheckSize, 0.0f, 
            -Vector2.up,
            modeData.groundCheckDistance, 
            groundLayers);

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
        if (!Application.isPlaying)
        {
            return;
        }

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(
            modeData.groundTestOrigin.position,
            modeData.groundTestOrigin.position - Vector3.up * modeData.groundCheckDistance);
    }

    private void LateUpdate()
    {
        // Ground debug
        if (grounded)
        {
            modeData.playerRenderer.color = Color.blue;
        }
        else
        {
            modeData.playerRenderer.color = Color.red;
        }
    }
}
