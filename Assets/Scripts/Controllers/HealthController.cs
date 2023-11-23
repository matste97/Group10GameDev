using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthController : MonoBehaviour
{

    public int dead = 0;
    public int playerHealth;
    public Animator animator;
    [SerializeField] private Image[] hearts;


   

     public void Start()
     {
         UpdateHealth();
         animator.SetBool("Dead", false);

    }

    // Update is called once per frame
    public void UpdateHealth()
   {
        for(int i = 0; i < hearts.Length; i++)
        {
            if (i < playerHealth)
            {
                hearts[i].color = Color.red;
            }
            else
            {
                hearts[i].color = Color.black;
                
            }
        }
    }

    public void FixedUpdate()
    {
        if (dead == playerHealth)
        {
            animator.SetBool("Dead", true);
            GetComponent<Move>().enabled = false;
            GetComponent<Jump>().enabled = false;

        }

        if (0 < playerHealth)
        {
            animator.SetBool("Dead", false);
        }
        
    }

}
