using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{

    [SerializeField]
    protected float speed;

    protected Vector3 target;
    protected Vector3 velocity;
    protected Vector3 previousPosition;
    protected bool flipped;
    
    [SerializeField]
    public Transform[] waypoints;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public virtual void Init(){
        target = waypoints[1].position;
    }


    public IEnumerator SetTarget(Vector3 position)
    {
        yield return new WaitForSeconds(2f);
        target = position;
        FaceTowards(position - transform.position);
    }

    public void FaceTowards(Vector3 direction)
    {
        if (direction.x < 0f)
            transform.localEulerAngles = new Vector3(0,180,0);
        else
        {
            transform.localEulerAngles = new Vector3(0,0,0);
        }
    }


    public void Movement()
    {
        velocity = ((transform.position - previousPosition) / Time.deltaTime);
        previousPosition = transform.position;

        if(transform.position != target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
        else
        {
            // Toggle between waypoints
            if (target == waypoints[0].position)
            {
                StartCoroutine("SetTarget", waypoints[1].position);
            }
            else
            {
                StartCoroutine("SetTarget", waypoints[0].position);
            }
        
        
    


            // if (target == waypoints[0].position)
            // {
            //     if (flipped)
            //     {
            //         flipped = !flipped;
            //         StartCoroutine("SetTarget", waypoints[1].position);
            //     }
            // }
            // else
            // {
            //     if(!flipped)
            //     {
            //         flipped = !flipped;
            //         StartCoroutine("SetTarget", waypoints[0].position);
            //     }
            // }

            
        }


    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
}
