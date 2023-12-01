using System.Collections;
using UnityEngine;

public class EnemyShooterScript : MonoBehaviour
{
    public float playerDetectionDistance = 10f;
    public GameObject projectilePrefab;
    public float projectileSpeed = 5f;
    public float timeBetweenShots = 5f; // Time between shots

    private Transform playerSensor;
    private bool canShoot = true; // Variable to control shooting frequency
    private Enemy enemyScript; // Reference to the Enemy script
    private Animator enemyAnimator; 

    [SerializeField] private AudioClip shootSound;

    void Start()
    {
        playerSensor = transform.Find("Player Sensor");
        
        enemyScript = GetComponent<Enemy>();
        enemyAnimator = GetComponent<Animator>(); 
    }

    void Update()
    {
        Vector2 rayDirection = enemyScript.isMovingRight ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, playerDetectionDistance, LayerMask.GetMask("Player"));


        Debug.DrawRay(transform.position, rayDirection * playerDetectionDistance, Color.green);

        if (hit.collider){
            StopEnemyMovement();
        }

        if (hit.collider != null && canShoot)
        {
            StopEnemyMovement();
            FireBullet(rayDirection);
            StartCoroutine(ShootCooldown());
        }
        else
        {
            ResumeEnemyMovement();
        }
    }

    IEnumerator ShootCooldown()
    {
        canShoot = false; // Prevent shooting until delay is over
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true; // Allow shooting again after the delay
    }

    void FireBullet(Vector2 shootingDirection)
    {
        enemyAnimator.SetTrigger("Shoot"); 
        GameObject newBullet = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Bullet bulletComponent = newBullet.GetComponent<Bullet>();

            if (shootSound != null)
    {
        AudioSource.PlayClipAtPoint(shootSound, transform.position);

    }

        if (bulletComponent != null)
        {
            bulletComponent.direction = shootingDirection; // Consider enemy's orientation
            bulletComponent.speed = projectileSpeed;
        }
    }
    void StopEnemyMovement()
    {
        enemyScript.enabled = false; // Disable the Enemy script
    }

    void ResumeEnemyMovement()
    {
        enemyScript.enabled = true; // Enable the Enemy script
    }
}
