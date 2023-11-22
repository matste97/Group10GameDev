using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private InputController input = null;
    [SerializeField, Range(0f, 100f)] private float maxSpeed = 4f;
    [SerializeField, Range(0f, 100f)] private float maxAcceleration = 35f;
    [SerializeField, Range(0f, 100f)] private float maxAirAcceleration = 20f;

    private Vector2 direction, desiredVelocity, velocity;
    private Rigidbody2D body;
    private Ground ground;

    private float maxSpeedChange, acceleration;
    private bool onGround;
    float horizontalMove = 0f;
    float inputHorizontal;
    float inputVertical;
    bool facingRight = true;

    private void Awake()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        ground = GetComponent<Ground>();
    }

    private void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * maxSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));


        direction.x = input.RetrieveMoveIntput();
        desiredVelocity = new Vector2(direction.x, 0f) * Mathf.Max(maxSpeed - ground.GetFriction(), 0f);

    }

    private void FixedUpdate()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");

        if (inputHorizontal != 0)
        {
            body.AddForce(new Vector2(inputHorizontal * maxSpeed, 0f));
        }

        if (inputHorizontal > 0 && !facingRight)
        {
            Flip();
        }

        if (inputHorizontal < 0 && facingRight)
        {
            Flip();
        }


        onGround = ground.GetOnGround();
        velocity = body.velocity;

        acceleration = onGround ? maxAcceleration : maxAirAcceleration;
        maxSpeedChange = acceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);

        body.velocity = velocity;

    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;

    }


}