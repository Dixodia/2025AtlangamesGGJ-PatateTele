using UnityEngine;
using UnityEngine.UI;

public class Bubble : MonoBehaviour
{
    public Color bubbleColor;        // Color of the bubble
    public float displayDuration;    // How long the bubble stays on screen

    private Rigidbody rb;          // Reference to Rigidbody2D component for movement

    public float upwardDeceleration = 0.1f; // Speed at which upward velocity decreases over time

    // Constructor for creating a bubble
    public void setBubble(GameObject bubblePrefab, Transform parentTransform, Color color, float duration, float initialUpwardSpeed)
    {
        bubbleColor = color;
        displayDuration = duration;

        rb = gameObject.GetComponent<Rigidbody>();

        // Set the initial upward velocity
        rb.linearVelocity = new Vector2(0, initialUpwardSpeed);

        // Set the duration before it disappears
        Destroy(gameObject, displayDuration);  // Destroy after a set duration (or adjust for your needs)
    }

    void Update()
    {
        // Apply deceleration to slow down the upward movement over time
        if (rb != null)
        {
            if(rb.linearVelocity.y > 0)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y - upwardDeceleration * Time.deltaTime);
            else rb.linearVelocity = Vector2.zero;
        }
    }

    // Optionally: You could add a method to destroy it manually
    public void DestroyBubble()
    {
        Object.Destroy(gameObject);
    }
}
