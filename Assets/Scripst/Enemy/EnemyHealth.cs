using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public int maxHealth = 3;
    private int currentHealth;
    public float bounceForce = 10.0f;
    


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision with: " + other.gameObject.name);
        // Check for collision with the player.
        if (other.gameObject.CompareTag("Player"))
        {
            TakeDamage(1);
            
            // Get the player's Rigidbody2D (assuming the player has one)
            Rigidbody2D playerRigidbody = other.GetComponent<Rigidbody2D>();
            
            if (playerRigidbody != null)
            {
                // Calculate the direction from the player to the enemy and apply a force in the opposite direction
                Vector2 bounceDirection = Vector2.up;
                playerRigidbody.AddForce(bounceDirection * bounceForce, ForceMode2D.Impulse);
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