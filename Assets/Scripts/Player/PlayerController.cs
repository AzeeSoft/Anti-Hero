﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameObject Floor;
    public Transform LeftLeg;
    public Transform RightLeg;
    private Rigidbody2D rb2d;
    private SpriteRenderer sp;

    public int walk;
    public int jump;
    public int dash;
    public int dashTime;
    public float groundCheckDist;

    private int dashTemp;
    private bool grounded;
    private bool right = true;
    private bool left = true;
    private float inputHorizontal;
    private bool inputJump;
    private bool inputDash;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        dashTemp = dashTime;
    }

    private void OnDrawGizmos()
    {
        Vector3 leftEnd = new Vector3(LeftLeg.position.x, LeftLeg.position.y - groundCheckDist, LeftLeg.position.z);
        Gizmos.DrawLine(LeftLeg.position, leftEnd);

        Vector3 rightEnd = new Vector3(RightLeg.position.x, RightLeg.position.y - groundCheckDist, RightLeg.position.z);
        Gizmos.DrawLine(RightLeg.position, rightEnd);
    }

    void Update()
    {
        GetInputs();
    }

    void FixedUpdate()
    {
        Move();
        TryJump();
        Dash();
    }

    void GetInputs()
    {
        inputHorizontal = Input.GetAxis("Horizontal");
        inputJump = Input.GetButtonDown("Jump");
        inputDash = Input.GetButtonDown("Dash");
    }

    void Dash()
    {
        Vector2 movementDash = new Vector2(inputHorizontal*dash,0);
        dashTime = dashTemp;
        if (inputDash)
        {
            while (dashTime != 0)
            {
                transform.Translate(movementDash * Time.fixedDeltaTime);
                dashTime--;
            }
        }
    }

    void Move()
    {
        Vector2 movement = new Vector2(inputHorizontal * walk, 0);
        transform.Translate(movement * Time.fixedDeltaTime);

        Vector3 newScale = HelperUtilities.CloneVector3(transform.localScale);
        if (inputHorizontal > 0.0) right = true;
        if (inputHorizontal < 0.0) left = true;
        if (inputHorizontal > 0.0f && right)
        {
            left = false;
            newScale.x = Mathf.Abs(newScale.x);
            right = true;
        }
        else if(inputHorizontal < 0.0f && left)
        {
            right = false;
            newScale.x = Mathf.Abs(newScale.x) * -1;
            left = true;
        }
        transform.localScale = newScale;
    }
    
    void TryJump()
    {
        RaycastHit2D groundCheckerL = Physics2D.Raycast(LeftLeg.position, Vector2.down, groundCheckDist);
        RaycastHit2D groundCheckerR = Physics2D.Raycast(LeftLeg.position, Vector2.down, groundCheckDist);
        grounded = groundCheckerL.collider != null || groundCheckerR.collider != null;

        if (grounded && inputJump)
        {
            Vector3 velocity = HelperUtilities.CloneVector3(rb2d.velocity);
            velocity.y = 0;
            rb2d.velocity = velocity;
            rb2d.AddForce(Vector2.up * jump);
        }
    }
}