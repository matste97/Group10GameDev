using UnityEngine;

public class EnableEnemy : MonoBehaviour
{
    private GameObject parentObject;
    private bool scriptsEnabled = false;

    void Start()
    {
        // Get the parent GameObject
        parentObject = transform.parent.gameObject;

        // Disable all scripts on the parent object initially
        DisableScriptsOnParent();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Assuming the player has a "Player" tag
        {
            EnableScriptsOnParent();
        }
    }

        void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DisableScriptsOnParent();
        }
    }

    void DisableScriptsOnParent()
    {
        MonoBehaviour[] scripts = parentObject.GetComponentsInChildren<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            if (script != this) // Exclude this script from disabling
            {
                script.enabled = false;
            }
        }
    }

    void EnableScriptsOnParent()
    {
        MonoBehaviour[] scripts = parentObject.GetComponentsInChildren<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            if (script != this) // Exclude this script from enabling
            {
                script.enabled = true;
            }
        }
    }
}