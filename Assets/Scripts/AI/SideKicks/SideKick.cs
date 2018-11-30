using System.Collections;
using System.Collections.Generic;
using AI.SideKickStates;
using UnityEngine;

public class SideKick : MonoBehaviour
{
//    public bool InitiallyFacingRight = true;

    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;

    public float playerDetectionRange = 80f;
    public float playerAttackRange = 40f;
    public float shootInterval = 1f;

    public bool showGizmos = false;

    private StateMachine<SideKick> _stateMachine;
    [HideInInspector] public SideKickController Controller;

    public Patrol.StateData PatrolStateData;
    public AttackPlayer.StateData AttackStateData;

    void OnDrawGizmos()
    {
        if (!showGizmos)
            return;

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, playerDetectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerAttackRange);
    }

    void Awake()
    {
        Controller = GetComponent<SideKickController>();

        _stateMachine = new StateMachine<SideKick>(this);
        _stateMachine.SwitchState(Idle.Instance);
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
//            Debug.Log("Attacking Player");
        }
    }


    public bool IsPlayerInDetectionRange()
    {
        float dist = Vector2.Distance(transform.position,
            LevelManager.Instance.GetPlayerGameObject().transform.position);
        if (dist <= playerDetectionRange)
        {
            return true;
        }

        return false;
    }

    public bool IsPlayerInAttackRange()
    {
        float dist = Vector2.Distance(transform.position,
            LevelManager.Instance.GetPlayerGameObject().transform.position);
        if (dist <= playerAttackRange && IsFacingPlayer())
        {
            return true;
        }

        return false;
    }

    public bool IsFacingPlayer()
    {
        if (transform.localScale.x > 0 &&
            transform.position.x > LevelManager.Instance.GetPlayerGameObject().transform.position.x)
        {
            return true;
        }
        if (transform.localScale.x < 0 &&
            transform.position.x < LevelManager.Instance.GetPlayerGameObject().transform.position.x)
        {
            return true;
        }

        return false;
    }

    public void SwitchToIdleState()
    {
        _stateMachine.SwitchState(Idle.Instance);
    }

    public void SwitchToApproachPlayerState()
    {
        _stateMachine.SwitchState(ApproachPlayer.Instance);
    }

    public void SwitchToAttackPlayerState()
    {
        _stateMachine.SwitchState(AttackPlayer.Instance);
    }

    public void ShootBullet()
    {
        Bullet bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation).GetComponent<Bullet>();
        bullet.dir = -transform.right * Mathf.Sign(transform.localScale.x);
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}