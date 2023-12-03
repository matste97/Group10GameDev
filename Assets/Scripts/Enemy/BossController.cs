using System.Collections;
using UnityEngine;

public class BossFollow : MonoBehaviour
{
    public Transform player;
    public bool flip;
    public float speed;
    public float baseStoppingDistance = 2f;
    public float attackCooldown = 2f;

    private Animator animator;
    private bool isAttacking;
    private float attackTimer;

    private Collider2D weaponCollider;

    [SerializeField] private AudioClip attackSound; 

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        weaponCollider = transform.Find("WeaponCollider").GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 scale = transform.localScale;

        float horizontalDistance = Mathf.Abs(player.position.x - transform.position.x);
        float verticalDistance = Mathf.Abs(player.position.y - transform.position.y);
        float adjustedStoppingDistance = baseStoppingDistance + verticalDistance * 0.7f;

        if (horizontalDistance <= adjustedStoppingDistance)
        {
            if (!isAttacking)
            {
                StartCoroutine(AttackRoutine());
            }
            SetIsMoving(false);
        }
        else
        {
            if (Time.time >= attackTimer)
            {
                if (player.position.x > transform.position.x)
                {
                    scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
                    transform.Translate(speed * Time.deltaTime, 0, 0);
                    SetIsMoving(true);
                }
                else
                {
                    scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
                    transform.Translate(speed * Time.deltaTime * -1, 0, 0);
                    SetIsMoving(true);
                }
            }
        }

        transform.localScale = scale;
    }

    // Coroutine for handling the attack animation and cooldown
    IEnumerator AttackRoutine()
    {
        isAttacking = true;

        // Choose a random attack animation
        int randomAttack = Random.value < 0.5f ? 1 : 2;
        SetAttackAnimation(randomAttack);

        SetIsAttacking(true); // Trigger the chosen attack animation


        

        yield return new WaitForSeconds(0.4f); // Adjust the duration of the attack animation

        SetIsAttacking(false); // Stop the attack animation
        isAttacking = false;
        weaponCollider.enabled = false;

        // Set a cooldown for the next attack
        yield return new WaitForSeconds(attackCooldown);
        attackTimer = Time.time + attackCooldown;
    }

    // Helper function to set the attack animation in the Animator
    void SetAttackAnimation(int attackNumber)
    {
        animator.SetBool("Attack1", attackNumber == 1);
        animator.SetBool("Attack2", attackNumber == 2);

        // Clear the other boolean parameter
        if (attackNumber == 1)
        {
            animator.SetBool("Attack2", false);

        }
        else if (attackNumber == 2)
        {
            animator.SetBool("Attack1", false);
        }
    }

    // Helper function to set the "isMoving" parameter in the Animator
    void SetIsMoving(bool moving)
    {
        animator.SetBool("isMoving", moving);
    }

    // Helper function to set the "isAttacking" parameter in the Animator
    void SetIsAttacking(bool attacking)
    {
        animator.SetBool("isAttacking", attacking);

        // If the boss is hit during an attack, trigger the "isTakingHit" animation
        if (attacking)
        {
            StartCoroutine(ResetTakingHitAnimation());
        }
    }

    // Coroutine to reset the "isTakingHit" animation after a short delay
    IEnumerator ResetTakingHitAnimation()
    {
        yield return new WaitForSeconds(0.5f); 
        animator.SetBool("isTakingHit", false);
    }

    void EnableWeaponCollider() {
        if (attackSound != null)
        {
            AudioSource.PlayClipAtPoint(attackSound, transform.position);
        }
        weaponCollider.enabled = true;
    }
        void disableWeaponCollider() {
        weaponCollider.enabled = false;
    }
}


