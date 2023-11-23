using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    [SerializeField] private int damage;

    [SerializeField] private HealthController _healthController;

    public Animator animator;
    private bool canTakeDamage = true;
    public float invCooldown = 1.0f;
    private float cooldownTimer = 0f; // Time between damage


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && canTakeDamage)
        {
            Damage();
        }
    }

    void Damage()
    {
        _healthController.playerHealth = _healthController.playerHealth - damage;
        cooldownTimer = invCooldown;
        canTakeDamage = false;
        print(damage);
        _healthController.UpdateHealth();
        animator.SetBool("Hurt", true);
    }

    private void Update()
    {
        if (!canTakeDamage)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0)
            {
                canTakeDamage = true;
                animator.SetBool("Hurt", false);
            }
        }
    }

}



