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
    private Collider2D axeCollider;

    private bool isMoving = true;
    private void Start()
    {
        animator = GetComponent<Animator>();
        enemyFollow = GetComponent<EnemyFollow>();
        axeCollider = transform.Find("AxeCollider").GetComponent<Collider2D>();
    }

    public void EnableAxeCollider()
    {
        axeCollider.enabled = true;
    }

    // Animation Event function called from the animation timeline when the axe swing is finished
    public void DisableAxeCollider()
    {
        axeCollider.enabled = false;
    }

    

    private void Attack()
    {
        canAttack = false;
        enemyFollow.enabled = false;
        animator.SetBool("IsMoving", isMoving = false);
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
            animator.SetBool("IsMoving", isMoving = true);
        }
        
        else
        {
            Debug.Log("out of range");
        }
    }
}
