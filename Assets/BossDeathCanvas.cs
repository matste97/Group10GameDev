using UnityEngine;

public class BossDeathCanvas : MonoBehaviour
{
    private GameObject bossDeathCanvas;
    private BossHealth bossHealth;

   void Start()
{
    bossDeathCanvas = gameObject;

    if (bossDeathCanvas != null)
    {
        bossDeathCanvas.SetActive(false);
    }

    bossHealth = GetComponentInParent<BossHealth>();

    if (bossHealth != null)
    {
        bossHealth.OnBossDeath += ShowCanvas;
        Debug.Log("Subscribed to OnBossDeath event");
    }
    else
    {
        Debug.LogError("BossHealth component not found");
    }
}

    void OnDestroy()
    {
        // Unsubscribe from the boss death event to avoid memory leaks
        if (bossHealth != null)
        {
            bossHealth.OnBossDeath -= ShowCanvas;
        }
    }

    void ShowCanvas()
    {
        // Boss is dead, show the canvas
        if (bossDeathCanvas != null)
        {
            Debug.Log("ShowCanvas() called");
            bossDeathCanvas.SetActive(true);
        }
    }
}
