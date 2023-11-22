using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f; // Adjust speed as needed
    public Vector2 direction; // Bullet direction

    void Update()
    {
        // Move the bullet in its direction
        transform.Translate(direction * speed * Time.deltaTime);

        // Destroy the bullet if it goes off-screen
        if (!GetComponent<Renderer>().isVisible)
        {
            Destroy(gameObject);
        }
    }

    // Destroy the bullet upon collision with an enemy or other designated objects
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") && !collision.CompareTag("Ignore"))
        {
            Destroy(gameObject);
            // Add enemy damage or any other logic here
        }
    }
}
