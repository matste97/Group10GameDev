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
// Calculate the direction from the player to the enemy
Vector2 bounceDirection = (transform.position - other.transform.position).normalized;
bounceDirection.y = Mathf.Abs(bounceDirection.y); // Make sure the bounce is always upwards

// Set the player's velocity to achieve a consistent upward bounce
playerRigidbody.velocity = bounceDirection * bounceForce;
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
