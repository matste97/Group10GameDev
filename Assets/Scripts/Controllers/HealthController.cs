using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;


public class HealthController : MonoBehaviour
{

    public int dead = 0;
    public int playerHealth;
    public Animator animator;
    [SerializeField] private Image[] hearts;
    public String currentScene; //TEMP FOR RELOAD LEVEL ON DEATH




    public void Start()
     {
        
         UpdateHealth();
         animator.SetBool("Dead", false);
         currentScene = SceneManager.GetActiveScene().name; //TEMP FOR RELOAD LEVEL ON DEATH

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


            StartCoroutine(ReloadSceneWithDelay()); //TEMP FOR RELOAD LEVEL ON DEATH

        }

        if (0 < playerHealth)
        {
            animator.SetBool("Dead", false);
            
        }
        
    }

    private IEnumerator ReloadSceneWithDelay() //TEMP FOR RELOAD LEVEL ON DEATH
    {
        yield return new WaitForSeconds(2f); 
        SceneManager.LoadScene(currentScene);
    }

}
