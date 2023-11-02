using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{

    Rigidbody2D myRigidBody;
    private bool falling = false;

    private float stopped = 0.0f;
    private float moveSpeed;
    [SerializeField] protected float speed;

    [SerializeField] private bool useFloorCheck = true;


    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = speed;
        myRigidBody = GetComponent<Rigidbody2D>();
        
    }

    //turn when collider detects wall ahead
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody.velocity.x)), 1f);
        }
        if (other.CompareTag("Floor"))
        {
            falling = false;
        }

        if (other.CompareTag("Player"))
        {
            moveSpeed = stopped;
        }
    }


    //turn when collider doesn't detect floor ahead
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Floor") && useFloorCheck == true)
        {
            transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody.velocity.x)), 1f);
        }
        if (other.CompareTag("Floor"))
        {
            falling = true;
        }

        if (other.CompareTag("Player"))
        {
            moveSpeed = speed;
        }
    }

    private bool isFacingRight()
    {
        return transform.localScale.x > 0;
    }

    private void Movement()
    {
            if (isFacingRight())
            {
                myRigidBody.velocity = new Vector2(moveSpeed, 0f);
                if(falling)
                {
                    myRigidBody.velocity = new Vector2(moveSpeed / 2, 0f);
                }
            }
            else
            {
                myRigidBody.velocity = new Vector2(-moveSpeed, 0f);
                if(falling)
                {
                    myRigidBody.velocity = new Vector2(-moveSpeed / 2 , 0f);
                }
            }

    }


    // Update is called once per frame
    void Update()
    {
        Movement();
    }
}