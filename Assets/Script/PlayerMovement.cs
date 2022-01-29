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
    private float jumpTime;
    [SerializeField]
    private float gravity;

    private bool jumping;
    private float timeJumped;



    void Update()
    {
        float xMove = Input.GetAxis("Horizontal");

        transform.position += new Vector3(xMove, 0.0f, 0.0f) * moveSpeed * Time.deltaTime;

        if (Input.GetButtonDown("Jump"))
        {
            jumping = true;
        }

        if (jumping)
        {
            transform.position += new Vector3(0.0f, jumpSpeed, 0.0f) * Time.deltaTime;

            timeJumped += Time.deltaTime;

            if (timeJumped > jumpTime)
            {
                jumping = false;
                timeJumped = 0.0f;
            }
        }

        transform.position += new Vector3(0.0f, -gravity * 9.8f, 0.0f) * Time.deltaTime;
    }
}
