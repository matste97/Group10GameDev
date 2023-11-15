using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    public GameObject player;

    public float attackRange;



    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {

        Vector3 scale = transform.localScale;
        
        if(Vector3.Distance(player.transform.position, transform.position) < attackRange) {
            Debug.Log("Attack");
        }
        else {
            Debug.Log("out of range");
        }


    }
}
