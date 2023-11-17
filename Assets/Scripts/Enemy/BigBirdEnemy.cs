using UnityEngine;
using System.Collections;
public class BigBirdEnemy : MonoBehaviour
{
    [SerializeField] private bool debugRaycasts = true;
    public float hitFloorDistance = 0.1f;
    public float moveSpeed = 1f;
    public float maxFallDistance = 15f;
    public float moveBackSpeed = 2f;

    private bool isFalling = false;
    private bool isMovingBack = false;
    private Vector3 startPosition;
    private Transform floorSensor;
    private Collider2D fallCollider;
    private Animator animator;
    private Rigidbody2D rb; 



    void Start()
    {
        floorSensor = transform.Find("FloorSensor");
        startPosition = transform.position;
        fallCollider = transform.Find("FallCollider").GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>(); 
    }

    void Update()
    {
        bool hitFloor = Physics2D.Raycast(floorSensor.position, Vector2.down, hitFloorDistance);

        if (isFalling)
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
             //rb.AddForce(Vector2.down * moveSpeed * Time.deltaTime, ForceMode2D.Impulse);
            

            if (transform.position.y <= startPosition.y - maxFallDistance || hitFloor)
            {
                isFalling = false;
                animator.SetBool("IsFalling", false);
                animator.SetBool("IsGroundPound", true);
                StartCoroutine(PlayGroundPoundAndWait());
            }
        }

        if (isMovingBack)
        {
            //rb.MovePosition(Vector2.MoveTowards(transform.position, startPosition, moveBackSpeed * Time.deltaTime));
            transform.position = Vector3.MoveTowards(transform.position, startPosition, moveBackSpeed * Time.deltaTime);

            if (transform.position == startPosition)
            {
                isMovingBack = false;
            }
        }

        if (debugRaycasts)
        {
            Debug.DrawRay(floorSensor.position, Vector2.down * hitFloorDistance, Color.red);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isFalling && !isMovingBack)
        {
            isFalling = true;
            animator.SetBool("IsFalling", true);
        }
    }

IEnumerator PlayGroundPoundAndWait()
{
    // Wait for the ground pound animation to finish
    while (animator.GetCurrentAnimatorStateInfo(0).IsName("GroundPound"))
    {
        yield return null;
    }

    yield return new WaitForSeconds(1.0f); // Wait for 1 second on the ground

    // When the delay is over, start moving back
    animator.SetBool("IsGroundPound", false);
    isMovingBack = true;
}
}
