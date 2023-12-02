using System.Collections;
using TMPro;
using UnityEngine;

public class BlinkingText : MonoBehaviour
{
    public TextMeshProUGUI textField;

    private Color originalColor;
    private bool isBlinking = false;

    void Start()
    {
        if (textField == null)
        {
            Debug.LogError("TextMeshProUGUI component not assigned. Please assign it in the inspector.");
            return;
        }

        // Save the original color of the text
        originalColor = textField.color;

        // Start the blinking coroutine
        StartCoroutine(BlinkText());
    }

    IEnumerator BlinkText()
    {
        isBlinking = true;

        // Blink random colors for 3 seconds
        float blinkDuration = 3f;
        float startTime = Time.time;

        while (Time.time - startTime < blinkDuration)
        {
            // Random color for blinking
            Color randomColor = new Color(Random.value, Random.value, Random.value);

            // Set the text color
            textField.color = randomColor;

            // Wait for a short time before changing color again
            yield return new WaitForSeconds(0.1f);
        }

        // Return to the original color
        textField.color = originalColor;

        isBlinking = false;
    }
}
