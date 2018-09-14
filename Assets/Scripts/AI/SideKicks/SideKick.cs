using System.Collections;
using System.Collections.Generic;
using AI.SideKickStates;
using UnityEngine;

public class SideKick : MonoBehaviour
{
    public bool InitiallyFacingRight = true;

    private StateMachine<SideKick> _stateMachine;
    [HideInInspector] public SideKickController Controller;

    public Patrol.StateData PatrolStateData;

    void Awake()
    {
        Controller = GetComponent<SideKickController>();

        _stateMachine = new StateMachine<SideKick>(this);
        _stateMachine.SwitchState(Patrol.Instance, InitiallyFacingRight ? Vector3.right : Vector3.left);
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _stateMachine.Update();
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("DirFlipper"))
        {
            if (PatrolStateData != null)
            {
                PatrolStateData.CurDir.x *= -1;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Player"))
        {
            Debug.Log("Attacking Player");
        }
    }
}