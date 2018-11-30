using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private PlayerModel PM;
    private Animator anim;

    public Transform LeftLeg;
    public Transform RightLeg;

    public int walk;
    public int jump;
    public int dash;
    public float totalDash;
    public float groundCheckDist;
    public float availableDash;
    public float dashReplenish;
    public Text deathText;
    public Color dashColor;

    private Vector2 dashVector;
    private Color originalColor;
    private bool grounded;
    private bool facingRight = true;
    [HideInInspector] public bool isDashing = false;
    private bool isWalking = false;

    private float inputHorizontal;
    private bool inputJump;
    private bool inputDash;

    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider2D;

    void Awake()
    {
        PM = GetComponent<PlayerModel>();
        anim = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider2D = GetComponent<Collider2D>();

        originalColor = _spriteRenderer.color;
    }

    void Start()
    {
        if (deathText != null)
        {
            deathText.text = "";
        }
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
        UpdateState();

        Move();
        TryJump();

        UpdateAnimator();
    }

    void GetInputs()
    {
        inputHorizontal = Input.GetAxis("Horizontal");
        inputJump = Input.GetButtonDown("Jump");
        inputDash = Input.GetButtonDown("Dash");
    }

    void UpdateState()
    {
        RaycastHit2D groundCheckerL = Physics2D.Raycast(LeftLeg.position, Vector2.down, groundCheckDist);
        RaycastHit2D groundCheckerR = Physics2D.Raycast(LeftLeg.position, Vector2.down, groundCheckDist);
        grounded = groundCheckerL.collider != null || groundCheckerR.collider != null;
    }

    void Move()
    {
        if (inputDash && !isDashing && availableDash >= totalDash && LevelManager.Instance._isPlaying)
        {
            StartDashing();
        }

        Vector2 movement = new Vector2(inputHorizontal * walk, 0);
        if (isDashing)
        {
            movement = HelperUtilities.CloneVector3(dashVector);
            availableDash -= Time.fixedDeltaTime;
            if (availableDash < 0)
            {
                StopDashing();
            }
        }

        if (!isDashing)
        {
            if (availableDash < totalDash)
            {
                availableDash += dashReplenish;
            }
        }

        if (LevelManager.Instance._isPlaying)
        {
            transform.Translate(movement * Time.fixedDeltaTime);
        }

        Vector3 newScale = HelperUtilities.CloneVector3(transform.localScale);
        if (movement.x > 0.0) facingRight = true;
        if (movement.x < 0.0) facingRight = false;
        if (movement.x > 0.0f && facingRight)
        {
            newScale.x = Mathf.Abs(newScale.x);
        }
        else if (movement.x < 0.0f && !facingRight)
        {
            newScale.x = Mathf.Abs(newScale.x) * -1;
        }

        isWalking = Math.Abs(movement.x) > 0;
        transform.localScale = newScale;
    }

    void TryJump()
    {
        if (isDashing)
        {
            return;
        }

        if (grounded && inputJump)
        {
            Vector3 velocity = HelperUtilities.CloneVector3(PM.rb2d.velocity);
            velocity.y = 0;
            PM.rb2d.velocity = velocity;
            PM.rb2d.AddForce(Vector2.up * jump);
        }
    }

    void UpdateAnimator()
    {
        anim.SetBool("IsGrounded", grounded);
        anim.SetBool("IsWalking", isWalking);
        anim.SetBool("IsDashing", isDashing);
    }

    void StartDashing()
    {
        isDashing = true;
        _collider2D.isTrigger = true;
        _spriteRenderer.color = dashColor;

        //availableDash = totalDash;
        float dir = facingRight ? 1f : -1f;
        dashVector = new Vector2(dir * dash, 0);
        PM.rb2d.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
    }

    void StopDashing()
    {
        isDashing = false;
        _collider2D.isTrigger = false;
        _spriteRenderer.color = originalColor;

        PM.rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pitfall"))
        {
            deathText.text = "You Died!";
        }
    }
}