using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveData : MonoBehaviour
{
    public float moveSpeed;
    public float jumpSpeed;
    public float maxJumpTime;
    public float climbSpeed;
    public float gravityScale;
    public Rigidbody2D playerRigidbody;
    public float groundCheckDistance;
    public Transform groundTestOrigin;    
    public Vector3 groundCheckSize;
}
