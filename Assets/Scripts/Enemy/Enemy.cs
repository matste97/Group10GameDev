using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    [SerializeField] protected float moveSpeed;

    [SerializeField] private bool useFloorCheck = true;
    [SerializeField] private bool debugRaycasts = true;
    private bool isMovingRight = true;


     public float wallDetectionDistance = 0.5f;
    public float edgeDetectionDistance = 0.1f;
    private Transform wallSensor;
    private Transform edgeSensorRight;



    // Start is called before the first frame update
    void Start()
    {
        wallSensor = transform.Find("Wall Sensor");
        edgeSensorRight = transform.Find("Floor Sensor Right");
    }

    // Update is called once per frame
    void Update()
    {
        bool isNearWall = Physics2D.Raycast(wallSensor.position, isMovingRight ? Vector2.right : Vector2.left, wallDetectionDistance);
        bool isNearEdge = !Physics2D.Raycast(edgeSensorRight.position, Vector2.down, edgeDetectionDistance);
                // If near a wall or edge, change direction
        if (debugRaycasts)
        {
            Debug.DrawRay(edgeSensorRight.position, Vector2.down * edgeDetectionDistance, Color.red);
            Debug.DrawRay(wallSensor.position, isMovingRight ? Vector2.right : Vector2.left, Color.blue);
        }
        

        if (isNearWall || (isNearEdge && useFloorCheck))
        {
            isMovingRight = !isMovingRight;
            transform.localScale = new Vector3(isMovingRight ? 1 : -1, 1, 1);
        }

        // Move the enemy
        Vector3 movement = isMovingRight ? Vector3.right : Vector3.left;
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }
}