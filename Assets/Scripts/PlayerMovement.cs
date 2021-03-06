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
    private PlayerMode movementMode;

    private Rigidbody2D playerRigidbody;
    private float xMove;
    private float yMove;
    private bool jumping;
    private float currentJumpDuration;
    private bool grounded;
    public PlayerMoveData moveData;

    public bool canClimb;
    public bool isClimbing = false;

    private AudioSource climbSound;

    private void Start()
    {
        moveData = humanModeData;
        playerRigidbody = GetComponent<Rigidbody2D>();


        climbSound = gameObject.AddComponent<AudioSource>();
        climbSound.clip = Resources.Load("sfx/climbing") as AudioClip;
        climbSound.volume = 0.03f;
        climbSound.loop = true;
    }

    private void Update()
    {
        // update player movement characteristics depending on mode
        if (movementMode.RobotMode)
        {
            moveData = robotModeData;
        }
        else
        {
            moveData = humanModeData;
        }

        // Get horizontal movement
        xMove = Input.GetAxis("Horizontal");

        handleFlippingSpriteBasedOnDirection(xMove);

        handleClimbing();

        // Jump
        if (Input.GetButtonDown("Jump") && (grounded || canClimb))
        {
            jumping = true;
        }

        // Check jump duration
        if (jumping)
        {
            currentJumpDuration += Time.deltaTime;

            if (currentJumpDuration >= moveData.maxJumpTime)
            {
                jumping = false;
                currentJumpDuration = 0.0f;
            }
        }
    }

    private void FixedUpdate()
    {
        // Apply movement in physics, keeping vertical speed
        Vector2 moveVector = new Vector2(xMove * moveData.moveSpeed, yMove * moveData.climbSpeed);
        Vector2 moveVelocity = moveVector * Time.deltaTime;

        if (canClimb)
        {
            Vector3 desiredVelocity = new Vector2(0.0f, 0.0f) + moveVelocity;
            playerRigidbody.velocity = desiredVelocity;
        }
        else
        {
            Vector3 desiredVelocity = new Vector2(0.0f, playerRigidbody.velocity.y) + moveVelocity;
            playerRigidbody.velocity = desiredVelocity;
        }

        // Jump physics, keep horizontal speed
        if (jumping)
        {
            Vector2 jumpVelocity = new Vector3(0.0f, moveData.jumpSpeed, 0.0f) * Time.deltaTime;
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0.0f) + jumpVelocity;
        }

        // Ground test
        RaycastHit2D hit = Physics2D.BoxCast(
            moveData.groundTestOrigin.position,
            moveData.groundCheckSize, 0.0f,
            -Vector2.up,
            moveData.groundCheckDistance,
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
            moveData.groundTestOrigin.position,
            moveData.groundTestOrigin.position - Vector3.up * moveData.groundCheckDistance);
    }

    private void handleFlippingSpriteBasedOnDirection(float xMove)
    {
        float y = transform.localScale.y;
        float z = transform.localScale.z;
        if (xMove < 0.0f) {
            transform.localScale = new Vector3(-1.0f, y, z);
        }
        else if (xMove > 0.0f) {
            transform.localScale = new Vector3(1.0f, y, z);
        }
    }

    private void handleClimbing()
    {
        if (canClimb)
        {
            yMove = Input.GetAxis("Vertical");
            playerRigidbody.gravityScale = 0.0f;
            if (yMove != 0.0f && !isClimbing)
            {
                climbSound.Play();
                isClimbing = true;
            }
            else
            {
                //climbSound.Stop();
                //isClimbing = false;
            }
        }
        else
        {
            yMove = 0.0f;
            playerRigidbody.gravityScale = moveData.gravityScale;

            climbSound.Stop();
            isClimbing = false;
        }
    }
}
