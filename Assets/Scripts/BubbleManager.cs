using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.WSA;

public class BubbleManager : MonoBehaviour, IPointerDownHandler
{
    public GameObject[] bubblePrefabs;  // Prefab for creating bubbles
    public Transform bubbleParent;   // Parent for all bubbles (optional)

    private Bubble currentBubble;

    public float initialUpwardSpeed;

    public float xSpawnAmplitude;
    public float ySpawnAmplitude;

    public float ySpeedAmplitude;

    private GameObject currentPrefab;  // Prefab for creating bubbles
    private Color currentColor;  // Prefab for creating bubbles

    public float defaultBubbleDuration = 5f; // Default duration the bubble stays on screen

    public float upscaleSpeed;
    public float maxScale;

    private List<Bubble> activeBubbles = new List<Bubble>();  // List to store active bubbles

    void Start()
    {
        if (bubblePrefabs == null || bubblePrefabs.Length == 0)
        {
            Debug.LogError("Please assign all required references in the inspector.");
            return;
        }
        currentPrefab = bubblePrefabs[0];
        currentColor = Color.black;
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
        if (currentBubble != null && !currentBubble.launched)
        {
            Debug.Log("testgrow");
            if (Input.GetMouseButton(0) && currentBubble.transform.localScale.x < maxScale)
            {
                Debug.Log("growing");
                float newScale = Mathf.Min(maxScale, currentBubble.transform.localScale.x + upscaleSpeed * Time.deltaTime);
                currentBubble.transform.localScale = new Vector3(newScale, newScale, 1);
            }
            else
            {
                currentBubble.launchBubble();
            }
        }
    }

    // Method to show a new bubble
    public void OnPointerDown(PointerEventData evData)
    {
        Debug.Log("pop");
        ShowNextBubble();
    }

    public void ShowNextBubble()
    {
        Debug.Log(currentColor.ToString());
        Vector3 decalage = new Vector3(Random.Range(-xSpawnAmplitude, xSpawnAmplitude), Random.Range(-ySpawnAmplitude, ySpawnAmplitude));


        GameObject bubbleObject = Instantiate(currentPrefab, bubbleParent.position + decalage, Quaternion.identity);
        bubbleObject.transform.SetParent(bubbleParent.transform, true);

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