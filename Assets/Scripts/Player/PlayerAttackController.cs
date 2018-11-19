using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    public GameObject AttackHB;
    //public BoxCollider2D attack;

    void Start()
    {
        // attack = AttackHB.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (Input.GetButton("Attack"))
        {
            AttackHB.SetActive(true);
        }

        if (Input.GetButtonUp("Attack"))
        {
            AttackHB.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (Input.GetButton("Attack"))
        {
            if (collision.gameObject.tag == "Enemy")
            {
                Debug.Log("Ouch!");
            }
        }
    }
}