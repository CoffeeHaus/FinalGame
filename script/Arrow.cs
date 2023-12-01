using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody2D rb;
    public float pullForce = 10f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 4f);
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        
        
        if (collider.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collider.gameObject.GetComponent<Enemy>();
            Debug.Log("EnemyHit");
            enemy.TakeDamage();
            Rigidbody2D rb = collider.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Calculate direction from the collider to this object
                Vector2 direction = transform.position - collider.transform.position;
                direction.Normalize();

                // Apply a force towards this object
                rb.AddForce(direction * pullForce);
            }
            Destroy(gameObject);
        }
    }
}
