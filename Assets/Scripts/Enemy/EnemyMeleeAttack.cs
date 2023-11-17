using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    public GameObject player;
    public float attackRange = 2.0f;
    public int damage = 1;
    public float attackCooldown = 3.0f;
    private float cooldownTimer = 0f; // Time between attacks
    
    private Animator animator;
    private bool canAttack = true;
    private EnemyFollow enemyFollow;

    private void Start()
    {
        animator = GetComponent<Animator>();
        enemyFollow = GetComponent<EnemyFollow>();
    }

    

    private void Attack()
    {
        canAttack = false;
        enemyFollow.enabled = false;
        animator.SetTrigger("Attack"); // Trigger the attack animation

        // Deal damage to the player should go here

        cooldownTimer = attackCooldown;
        
    }

    // Start is called before the first frame update
    // Update is called once per frame
private void Update()
    {
        if (!canAttack)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0)
            {
                canAttack = true;
            }
        }

        if (Vector3.Distance(player.transform.position, transform.position) < attackRange && canAttack)
        {
            Attack();
        }
        if (Vector3.Distance(player.transform.position, transform.position) > attackRange){
            enemyFollow.enabled = true;
        }
        
        else
        {
            Debug.Log("out of range");
        }
    }
}
