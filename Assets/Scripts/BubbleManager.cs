using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.WSA;

public class BubbleManager : MonoBehaviour
{
    public GameObject[] bubblePrefabs;  // Prefab for creating bubbles
    public Transform bubbleParent;   // Parent for all bubbles (optional)

    protected Bubble currentBubble;

    public float initialUpwardSpeed;

    public float xSpawnAmplitude;
    public float ySpawnAmplitude;

    public float ySpeedAmplitude;

    [SerializeField] protected GameObject currentPrefab;  // Prefab for creating bubbles
    [SerializeField] protected Color currentColor;  // Prefab for creating bubbles

    public float defaultBubbleDuration = 5f; // Default duration the bubble stays on screen

    protected List<Bubble> activeBubbles = new List<Bubble>();  // List to store active bubbles

    protected virtual void Start()
    {
        if (bubblePrefabs == null || bubblePrefabs.Length == 0)
        {
            Debug.LogError("Please assign all required references in the inspector.");
            return;
        }
        currentPrefab = bubblePrefabs[0];
        currentColor = Color.black;
    }

    protected virtual void Update()
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
    }

    public void ShowNextBubble()
    {
        Vector3 decalage = new Vector3(Random.Range(-xSpawnAmplitude, xSpawnAmplitude), Random.Range(-ySpawnAmplitude, ySpawnAmplitude));


        GameObject bubbleObject = Instantiate(currentPrefab, bubbleParent.position + decalage, Quaternion.identity);
        //bubbleObject.transform.SetParent(bubbleParent.transform, true);

        // Create a new Bubble object
        currentBubble = bubbleObject.GetComponent<Bubble>();
        currentBubble.setBubble(bubbleObject, bubbleParent, currentColor, defaultBubbleDuration, initialUpwardSpeed + Random.Range(-ySpeedAmplitude, ySpeedAmplitude));

        // Add the new bubble to the list of active bubbles
        activeBubbles.Add(currentBubble);
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

    public void setColor(Color color)
    {
        currentColor = color;
        Debug.Log("Color set to : " + currentColor.ToString());
    }
}