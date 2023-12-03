using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] public int maxHealth = 3;
    private int currentHealth;
    public float bounceForce = 10.0f;
    public float damageCooldown = 0.2f;
    private float lastDamageTime = 0.0f;
    [SerializeField] private AudioClip deathSound;
    private Animator animator;
    private bool isDead = false;

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        // Stop the boss movement if it's dead
        if (isDead)
        {
            SetIsMoving(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision with: " + other.gameObject.name);

        if (other.gameObject.CompareTag("Player"))
        {
            if (!isDead && Time.time - lastDamageTime >= damageCooldown)
            {
                TakeDamage(1);
                lastDamageTime = Time.time;

                Rigidbody2D playerRigidbody = other.GetComponent<Rigidbody2D>();

                if (playerRigidbody != null)
                {
                    Vector2 bounceDirection = (other.transform.position - transform.position).normalized;
                    bounceDirection.y = Mathf.Max(bounceDirection.y, 0.5f);
                    bounceDirection.x = 0f;
                    playerRigidbody.velocity = new Vector2(bounceDirection.x, bounceDirection.y) * bounceForce;
                }
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
        else
        {
            // Trigger the "isTakingHit" animation in the parent (boss) Animator
            animator.SetBool("isTakingHit", true);
        }
    }

    private void Die()
    {
        isDead = true; // Set the flag to indicate the boss is dead

        // Trigger the "isDead" animation in the parent (boss) Animator
        animator.SetTrigger("isDead");

        if (deathSound != null)
        {
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
        }

        // Disable components that control boss behavior (e.g., movement script)
        BossFollow bossFollower = GetComponentInParent<BossFollow>();
    if (bossFollower != null)
    {
        bossFollower.enabled = false;
    }
    }

    // Helper function to set the "isMoving" parameter in the Animator
    void SetIsMoving(bool moving)
    {
        animator.SetBool("isMoving", moving);
    }
}
