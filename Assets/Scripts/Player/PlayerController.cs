using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject Floor;
    public int walk;
    public int run;
    public int jump;
    private bool grounded;
    private Rigidbody2D rb2d;
    private SpriteRenderer sp;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
    }


    void FixedUpdate() {
        grounded = rb2d.IsTouching(Floor.GetComponent<BoxCollider2D>());

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

        if (!(Input.GetButton("Run")) || !grounded)
        {
            transform.Translate(movementW * Time.fixedDeltaTime);
        }

        if (moveHorizontal != 0 && Input.GetButton("Run") && grounded) {
                transform.Translate((movementR) * Time.fixedDeltaTime);
        }

        if (grounded && Input.GetButtonDown("Jump")) {
            grounded = false;
            rb2d.AddForce(Vector2.up * jump);
        }

    }
}
