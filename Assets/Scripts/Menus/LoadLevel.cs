using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PickupTrigger : MonoBehaviour
{
    
    [SerializeField] private AudioClip deathSound; 

    public string nextLevelName; // Name of the next level to load
    public float delayBeforeLoad = 2f; // Delay in seconds before loading the next level

    private bool canLoadNextLevel = true; // Control variable to prevent multiple level loads

    // This method is called when the GameObject collides with another GameObject
    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.CompareTag("Player")&& canLoadNextLevel)
        {
                        if (deathSound != null)
    {
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
        // Or if you have an AudioSource attached to the enemy, use:
        // GetComponent<AudioSource>().PlayOneShot(deathSound);
    }

            canLoadNextLevel = false; // Prevent multiple level loads
            StartCoroutine(LoadNextLevelWithDelay());
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;

        }
        
    
    }
    private IEnumerator LoadNextLevelWithDelay()
    {
        yield return new WaitForSeconds(delayBeforeLoad); // Wait for the specified delay

        SceneManager.LoadScene(nextLevelName); // Load the next level
    }
}