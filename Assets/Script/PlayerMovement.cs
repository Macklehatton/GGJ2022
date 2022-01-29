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

    private bool jumping;
    private float currentJumpDuration;
    private bool grounded;

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float xMove = Input.GetAxis("Horizontal");

        transform.position += new Vector3(xMove, 0.0f, 0.0f) * moveSpeed * Time.deltaTime;
        
        if (Input.GetButtonDown("Jump") && grounded)
        {
            jumping = true;
        }

        if (jumping)
        {
            transform.position += new Vector3(0.0f, jumpSpeed, 0.0f) * Time.deltaTime;

            currentJumpDuration += Time.deltaTime;

            if (currentJumpDuration >= maxJumpTime)
            {
                jumping = false;
                grounded = false;
                currentJumpDuration = 0.0f;
            }
        }
    }

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, groundCheckDistance, LayerMask.NameToLayer("Environment"));

        if (hit.collider != null)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

    private void LateUpdate()
    {
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
