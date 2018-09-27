using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public float totalDash;
    public float groundCheckDist;
    public float availableDash;
    public float dashReplenish;

    private Vector2 dashVector;
    private bool grounded;
    private bool facingRight = true;
    private bool isDashing = false;

    private float inputHorizontal;
    private bool inputJump;
    private bool inputDash;

    public Text deathText;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        deathText.text = "";
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
    }

    void GetInputs()
    {
        inputHorizontal = Input.GetAxis("Horizontal");
        inputJump = Input.GetButtonDown("Jump");
        inputDash = Input.GetButtonDown("Dash");
    }

    void Move()
    {
        if (inputDash && !isDashing && availableDash >= totalDash)
        {
            isDashing = true;
            //availableDash = totalDash;
            float dir = facingRight ? 1f : -1f;
            dashVector = new Vector2(dir * dash, 0);
            rb2d.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }

        Vector2 movement = new Vector2(inputHorizontal * walk, 0);
        if (isDashing)
        {
            movement = HelperUtilities.CloneVector3(dashVector);
            availableDash -= Time.fixedDeltaTime;
            if (availableDash < 0)
            {
                isDashing = false;
                rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }

        if (!isDashing)
        {
            if (availableDash < totalDash)
            {
                availableDash += dashReplenish;
            }
        }

        transform.Translate(movement * Time.fixedDeltaTime);

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
        transform.localScale = newScale;
    }

    void TryJump()
    {
        if (isDashing)
        {
            return;
        }

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pitfall"))
        {
            deathText.text = "You Died!";
        }
    }
}