using System.Collections.Generic;
using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    public bool isActive = false;

    public GameObject[] bubblePrefabs;  // Prefab for creating bubbles
    public Transform bubbleParent;   // Parent for all bubbles (optional)

    public Bubble currentBubble;

    [SerializeField] public float initialUpwardSpeed;

    public float xSpawnAmplitude;
    public float ySpawnAmplitude;

    public float ySpeedAmplitude;

    [SerializeField] protected GameObject currentPrefab;  // Prefab for creating bubbles
    [SerializeField] protected Color currentColor;  // Prefab for creating bubbles

    [SerializeField] public float defaultBubbleDuration = 2f; // Default duration the bubble stays on screen

    public List<Bubble> activeBubbles = new List<Bubble>();  // List to store active bubbles

    protected virtual void Start()
    {
        if (bubblePrefabs == null || bubblePrefabs.Length == 0)
        {
            Debug.LogError("Please assign all required references in the inspector.");
            return;
        }
        currentPrefab = bubblePrefabs[0];
    }

    protected virtual void Update()
    {
        if (isActive)
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
    }

    public virtual void ShowNextBubble()
    {
        Vector3 decalage = generateDecalage();
        GameObject bubbleObject = Instantiate(currentPrefab, bubbleParent.position + decalage, Quaternion.identity);
        //bubbleObject.transform.SetParent(bubbleParent.transform, true);
        if (decalage.x < 0)
        {
            bubbleObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else bubbleObject.GetComponent<SpriteRenderer>().flipX = true;

        // Create a new Bubble object
        currentBubble = bubbleObject.GetComponent<Bubble>();
        currentBubble.setBubble(bubbleObject, bubbleParent, currentColor, defaultBubbleDuration, initialUpwardSpeed + Random.Range(-ySpeedAmplitude, ySpeedAmplitude));

        // Add the new bubble to the list of active bubbles
        activeBubbles.Add(currentBubble);
        storeBubbleValues(bubbleObject);
    }

    protected virtual void storeBubbleValues(GameObject bubbleObj)
    {

    }

    protected virtual Vector3 generateDecalage()
    {
        return new Vector3(Random.Range(-xSpawnAmplitude, xSpawnAmplitude), Random.Range(-ySpawnAmplitude, ySpawnAmplitude));
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