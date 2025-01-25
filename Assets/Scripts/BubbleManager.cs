using System.Collections.Generic;
using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    public GameObject[] bubblePrefabs;  // Prefab for creating bubbles
    public Transform bubbleParent;   // Parent for all bubbles (optional)
    public Color[] bubbleColors;     // Array of possible bubble colors

    public float initialUpwardSpeed;

    public float xSpawnAmplitude;
    public float ySpawnAmplitude;

    public float ySpeedAmplitude;

    private GameObject currentPrefab;  // Prefab for creating bubbles
    private Color currentColor;  // Prefab for creating bubbles

    public float defaultBubbleDuration = 5f; // Default duration the bubble stays on screen

    private List<Bubble> activeBubbles = new List<Bubble>();  // List to store active bubbles

    void Start()
    {
        if (bubblePrefabs == null || bubbleColors.Length == 0 || bubblePrefabs.Length == 0)
        {
            Debug.LogError("Please assign all required references in the inspector.");
            return;
        }
        currentPrefab = bubblePrefabs[0];
        currentColor = bubbleColors[0];
    }

    void Update()
    {
        // Check if any condition is met to remove bubbles (this can be customized)
        for (int i = activeBubbles.Count - 1; i >= 0; i--)
        {
            // Example condition: If the bubble has exceeded its lifetime, destroy it
            Bubble bubble = activeBubbles[i];
            if (bubble == null)
            {
                activeBubbles.RemoveAt(i);
            }
        }

        // Example of adding a new bubble by clicking
        if (Input.GetMouseButtonDown(0))  // Left mouse button click
        {
            ShowNextBubble();
        }
    }

    // Method to show a new bubble
    public void ShowNextBubble()
    {

        Vector3 decalage = new Vector3(Random.Range(-xSpawnAmplitude, xSpawnAmplitude), Random.Range(-ySpawnAmplitude, ySpawnAmplitude));

        GameObject bubbleObject = Instantiate(currentPrefab, bubbleParent.position + decalage, Quaternion.identity);
        bubbleObject.transform.SetParent(bubbleParent.transform, true);

        // Create a new Bubble object
        Bubble newBubble = bubbleObject.GetComponent<Bubble>();
        newBubble.setBubble(bubbleObject, bubbleParent, currentColor, defaultBubbleDuration, initialUpwardSpeed + Random.Range(-ySpeedAmplitude, ySpeedAmplitude));

        // Add the new bubble to the list of active bubbles
        activeBubbles.Add(newBubble);
    }

    // Optionally: Method to manually destroy a specific bubble
    public void DestroyBubble(Bubble bubble)
    {
        bubble.DestroyBubble();
        activeBubbles.Remove(bubble);
    }

    // Method to clear all active bubbles
    public void ClearAllBubbles()
    {
        foreach (Bubble bubble in activeBubbles)
        {
            bubble.DestroyBubble();
        }
        activeBubbles.Clear();
    }
}
