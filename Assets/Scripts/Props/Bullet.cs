using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 dir = Vector3.zero;
    public float speed = 3f;
    public int damage = 5;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Move();
	}

    void Move()
    {
        Vector3 newPos = transform.position + (dir.normalized * speed);
        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerModel playerModel = other.GetComponent<PlayerModel>();
            if (playerModel.TakeDamage(damage))
            {
                Destroy(gameObject);
            }
        }
    }
}
