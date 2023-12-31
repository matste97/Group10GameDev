using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    [SerializeField] private int damage;

    [SerializeField] private HealthController _healthController;

    [SerializeField] private AudioSource landingSoundEffect;

    public Animator animator;
    private bool canTakeDamage = true;
    public float invCooldown = 1.0f;
    private float cooldownTimer = 0f; // Time between damage

    private bool canHealDamage = true;
    public float healCooldown = 1.0f;
    private float healCooldownTimer = 0f; // Time between heals



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && canTakeDamage)
        {
            Damage();
        }
        if (collision.CompareTag("PowerUp"))
        {
            Heal();
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
        landingSoundEffect.Play();
    }


    void Heal()
    {
        if(_healthController.playerHealth <=2)
    {
        _healthController.playerHealth = _healthController.playerHealth + 1;
        cooldownTimer = healCooldown;
        canHealDamage = false;
        _healthController.UpdateHealth();
    }
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



