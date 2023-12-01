using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{

    [SerializeField] private AudioClip deathSound; 

    private void OnTriggerEnter2D(Collider2D collision)
    {

            if (deathSound != null)
    {
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
        // Or if you have an AudioSource attached to the enemy, use:
        // GetComponent<AudioSource>().PlayOneShot(deathSound);
    }

        if (collision.CompareTag("Player"))
        {
            
            Destroy(transform.gameObject);
            
        }

    }
}
