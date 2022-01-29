using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveData : MonoBehaviour
{
    public float moveSpeed;
    public float jumpSpeed;
    public float maxJumpTime;
    public Rigidbody2D playerRigidbody;
    public float groundCheckDistance;
    public SpriteRenderer playerRenderer;
    public Transform groundTestOrigin;    
    public Vector3 groundCheckSize;
}
