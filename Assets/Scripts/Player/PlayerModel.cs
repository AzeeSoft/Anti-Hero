using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerModel : MonoBehaviour
{
    public int MaxHealth = 100;

    [SerializeField] private int health = 100;

    [HideInInspector] public Rigidbody2D rb2d;
    [HideInInspector] public SpriteRenderer sp;

    private PlayerController playerController;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        playerController = GetComponent<PlayerController>();
    }

    public float GetHealth()
    {
        return health;
    }

    public float GetAvailableDash()
    {
        return playerController.availableDash;
    }

    public float GetTotalDash()
    {
        return playerController.totalDash;
    }

    bool CanTakeDamage()
    {
        return !playerController.isDashing;
    }

    public bool TakeDamage(int damage)
    {
        if (CanTakeDamage())
        {
            health -= damage;
            if (health < 0)
            {
                health = 0;
            }

            return true;
        }

        return false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("WinBox"))
        {
            LevelManager.Instance.GameWon();
        }
    }
}