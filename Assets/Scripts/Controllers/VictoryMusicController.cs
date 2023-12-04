using UnityEngine;

public class VictoryMusicController : MonoBehaviour
{
    public AudioClip victoryMusic;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // If there's no AudioSource component, add one
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Assign the victory music to the AudioSource
        audioSource.clip = victoryMusic;
    }

    private void Update()
    {
        // Check if the boss is dead
        BossHealth bossHealth = FindObjectOfType<BossHealth>();
        if (bossHealth != null && bossHealth.IsDead())
        {
            // Stop the camera's music (assuming the camera has an AudioSource component)
            Camera.main.GetComponent<AudioSource>().Stop();

            // Play the victory music
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }
}
