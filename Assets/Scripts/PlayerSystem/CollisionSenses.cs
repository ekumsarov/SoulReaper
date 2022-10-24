using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSenses : MonoBehaviour
{
    #region Check Transforms

    [SerializeField] private Transform _groundCheck;
    public Transform GroundCheck => _groundCheck;

    public float GroundCheckRadius { get => groundCheckRadius; set => groundCheckRadius = value; }
    public LayerMask WhatIsGround { get => whatIsGround; set => whatIsGround = value; }

    [SerializeField] private float groundCheckRadius;

    [SerializeField] private LayerMask whatIsGround;

    #endregion

    public bool Ground
    {
        get => Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, whatIsGround);
    }
}