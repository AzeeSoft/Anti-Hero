using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
//    public GameObject AttackHB;
    //public BoxCollider2D attack;

    private PlayerModel model;
    private PlayerController controller;

    void Awake()
    {
        model = GetComponent<PlayerModel>();
        controller = GetComponent<PlayerController>();
    }

    void Start()
    {
        // attack = AttackHB.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        /*if (Input.GetButton("Attack"))
        {
            AttackHB.SetActive(true);
        }

        if (Input.GetButtonUp("Attack"))
        {
            AttackHB.SetActive(false);
        }*/
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("SideKick"))
        {
            if (controller.isDashing)
            {
                SideKick sideKick = other.GetComponent<SideKick>();
                sideKick.Die();
            }
        }
    }
}