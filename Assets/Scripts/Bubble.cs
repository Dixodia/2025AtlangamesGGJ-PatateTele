using UnityEngine;
using UnityEngine.UI;

public class Bubble : MonoBehaviour
{
    public bool launched = false;

    public float displayDuration;    // How long the bubble stays on screen

    private Rigidbody rb;          // Reference to Rigidbody2D component for movement

    public float upwardDeceleration = 0.1f; // Speed at which upward velocity decreases over time

    private float initUpSpeed;
    // Constructor for creating a bubble

    public GameObject popParticlePrefab;

    public void setBubble(GameObject bubblePrefab, Transform parentTransform, Color color, float duration, float initialUpwardSpeed)
    {
        GetComponent<SpriteRenderer>().color = color;
        displayDuration = duration;
        initUpSpeed = initialUpwardSpeed;
        rb = gameObject.GetComponent<Rigidbody>();

        // Set the duration before it disappears
    }

    void Update()
    {

        // Apply deceleration to slow down the upward movement over time
        if (rb != null && launched)
        {
            if (rb.linearVelocity.y > 0)
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y - upwardDeceleration * Time.deltaTime);
            else rb.linearVelocity = Vector2.zero;
        }
    }

    public float launchBubble(bool timeLimit)
    {
        launched = true;
        // Set the initial upward velocity
        rb.linearVelocity = transform.up * initUpSpeed;
        if(timeLimit) Destroy(gameObject, displayDuration);  // Destroy after a set duration (or adjust for your needs)
        return transform.localScale.x;
    }

    // Optionally: You could add a method to destroy it manually
    public void DestroyBubble()
    {
        //Play anime 
        Destroy(gameObject, 0);
    }
}
