using UnityEngine;

public class Skeleton : Enemy
{
   
    private Rigidbody2D enemyRigidbody;
    private Transform playerTransform;
    public bool isActive;
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        health = 3;
        speed = 5f;
        GameObject player = GameManager.Instance.Player;
        if (player != null)
        {
            
            playerTransform = player.transform;
        }

        // Get the Rigidbody component
        enemyRigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (isActive && playerTransform != null)
        {
            MoveTowardsPlayer();
            //Debug.Log("move Skell");
        }
    }

    void MoveTowardsPlayer()
    {
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        enemyRigidbody.velocity = direction * speed;
        if (enemyRigidbody.velocity.x > 0)
        spriteRenderer.flipX = false;
        else if (enemyRigidbody.velocity.x < 0)
        spriteRenderer.flipX = true;
    }

    // Method to activate the enemy
    public void Activate()
    {
        isActive = true;
    }


}
