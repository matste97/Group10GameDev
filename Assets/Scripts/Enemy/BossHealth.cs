using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class BossHealth : MonoBehaviour
{
    [SerializeField] public int maxHealth = 3;
    private int currentHealth;
    public float bounceForce = 10.0f;
    public float damageCooldown = 0.2f;
    private float lastDamageTime = 0.0f;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private Image[] heartsb;
    private Animator animator;
    private bool isDead = false;

    public GameObject victoryScreen; 
    private AudioSource backgroundMusicAudioSource; // Reference to the main camera's audio source

    // Declare a delegate and event for boss death
    public delegate void BossDeathAction();
    public event BossDeathAction OnBossDeath;

    private void Start()
    {
        
        currentHealth = maxHealth;
        animator = GetComponentInParent<Animator>();
        // Find the main camera's AudioSource component
        backgroundMusicAudioSource = Camera.main.GetComponent<AudioSource>();
    }

    private void Update()
    {
        UpdateHealth();

        // Stop the boss movement if it's dead
        if (isDead)
        {
            SetIsMoving(false);
        }
    }

    public bool IsDead()
    {
        return isDead;
    }

    private void UpdateHealth()
    {
        for (int i = 0; i < heartsb.Length; i++)
        {
            if (i < currentHealth)
            {
                heartsb[i].color = new Color(0.3f, 0.05f, 0.31f);
            }
            else
            {
                heartsb[i].color = Color.black;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
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

        // Disable components that control boss behavior (e.g., movement script)
        BossFollow bossFollower = GetComponentInParent<BossFollow>();
        if (bossFollower != null)
        {
            bossFollower.enabled = false;
        }

        // Stop the background music
        if (backgroundMusicAudioSource != null)
        {
            backgroundMusicAudioSource.Stop();
        }

        // Play the death sound
        if (deathSound != null)
        {
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
        }

         EnableVictoryScreen();
         Invoke("LoadMainMenu", 10f);

    }

    // Helper function to set the "isMoving" parameter in the Animator
    void SetIsMoving(bool moving)
    {
        animator.SetBool("isMoving", moving);
    }

        // Method to enable the VictoryScreen
    private void EnableVictoryScreen()
    {
        if (victoryScreen != null)
        {
            victoryScreen.SetActive(true);
        }
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); 
    }
}
