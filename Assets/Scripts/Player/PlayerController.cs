using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject Floor;
    public Transform LeftLeg;
    public Transform RightLeg;
    private Rigidbody2D rb2d;
    private SpriteRenderer sp;

    public int walk;
    public int run;
    public int jump;
    public float groundCheckDist;

    private bool grounded;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
    }

    private void OnDrawGizmos()
    {
        Vector3 leftEnd = new Vector3(LeftLeg.position.x, LeftLeg.position.y - groundCheckDist, LeftLeg.position.z);
        Gizmos.DrawLine(LeftLeg.position, leftEnd);

        Vector3 rightEnd = new Vector3(RightLeg.position.x, RightLeg.position.y - groundCheckDist, RightLeg.position.z);
        Gizmos.DrawLine(RightLeg.position, rightEnd);
    }


    void FixedUpdate() {

        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector2 movementW = new Vector2(moveHorizontal * walk, 0);
        Vector2 movementR = new Vector2(moveHorizontal * run, 0);

        if (moveHorizontal > 0.1f) {
            sp.flipX = false;
        }
        if (moveHorizontal < -0.1f)
        {
            sp.flipX = true;
        }

        RaycastHit2D groundCheckerL = Physics2D.Raycast(LeftLeg.position, Vector2.down, groundCheckDist);
        RaycastHit2D groundCheckerR = Physics2D.Raycast(LeftLeg.position, Vector2.down, groundCheckDist);
        grounded = groundCheckerL.collider != null || groundCheckerR.collider != null;

        if (!(Input.GetButton("Run")) || !grounded)
        {
            transform.Translate(movementW * Time.fixedDeltaTime);
        }

        if (moveHorizontal != 0 && Input.GetButton("Run") && grounded) {
                transform.Translate((movementR) * Time.fixedDeltaTime);
        }

        if (grounded && Input.GetButtonDown("Jump")) {
            Vector3 velocity = HelperUtilities.CloneVector3(rb2d.velocity);
            velocity.y = 0;
            rb2d.velocity = velocity;
            rb2d.AddForce(Vector2.up * jump);
        }

    }
}