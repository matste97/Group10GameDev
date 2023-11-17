using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public int maxHealth = 3;
    private int currentHealth;
    public float bounceForce = 10.0f;
    public float damageCooldown = 0.2f; // Adjust the cooldown time as needed
    private float lastDamageTime = 0.0f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision with: " + other.gameObject.name);
        // Check for collision with the player.
        if (other.gameObject.CompareTag("Player"))
        {
            if (Time.time - lastDamageTime >= damageCooldown)
            {
                TakeDamage(1);
                lastDamageTime = Time.time;

                // Get the player's Rigidbody2D (assuming the player has one)
                Rigidbody2D playerRigidbody = other.GetComponent<Rigidbody2D>();

                if (playerRigidbody != null)
                {
                    Vector2 bounceDirection = (other.transform.position - transform.position).normalized;

                    // Ensure the minimum upward bounce and no horizontal movement
                    bounceDirection.y = Mathf.Max(bounceDirection.y, 0.5f);
                    bounceDirection.x = 0f;
                    // Set the player's velocity to achieve a consistent upward bounce
                    playerRigidbody.velocity = new Vector2(bounceDirection.x, bounceDirection.y) * bounceForce;
                }
            }
        }
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Add death behavior here (e.g., play an animation, destroy the enemy GameObject).
        Destroy(transform.parent.gameObject);
    }
}
