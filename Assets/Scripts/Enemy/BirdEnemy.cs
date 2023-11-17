using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float speed = 2.0f;

    private float startTime;
    private float journeyLength;

    private bool movingToEnd = true;
    private bool facingRight = true; // To track the object's facing direction

    void Start()
    {
        startTime = Time.time;
        journeyLength = Vector3.Distance(startPoint.position, endPoint.position);
    }

    void Update()
    {
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;

        if (movingToEnd)
        {
            transform.position = Vector3.Lerp(startPoint.position, endPoint.position, fracJourney);
            if (!facingRight)
            {
                Flip(); // Flip if not facing right
            }
        }
        else
        {
            transform.position = Vector3.Lerp(endPoint.position, startPoint.position, fracJourney);
            if (facingRight)
            {
                Flip(); // Flip if facing right
            }
        }

        if (fracJourney >= 1.0f)
        {
            movingToEnd = !movingToEnd;
            startTime = Time.time;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}