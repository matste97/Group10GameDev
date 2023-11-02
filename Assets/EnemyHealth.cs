using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public int maxHealth = 3;
    private int currentHealth;
    


    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check for collision with the player.
        if (other.gameObject.CompareTag("Player"))
        {
                TakeDamage(1);
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