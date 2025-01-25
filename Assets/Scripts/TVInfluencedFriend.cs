using UnityEngine;

public class TVInfluencedFriend : ConstantChatBubbleManager
{
    [SerializeField] Color baseColor;
    [SerializeField] Color influencedColor;

    public float influencePercentage = 0;
    [SerializeField] float influenceDecreaseRate;
    [SerializeField] float bubbleScale;
    protected override void Update()
    {
        if (Time.realtimeSinceStartup > nextBubbleTimer)
        {
            currentColor = choseColor();
            ShowNextBubble();
            currentBubble.transform.localScale = new Vector3(bubbleScale, bubbleScale, 1);
            currentBubble.launchBubble();
            nextBubbleTimer = generateNextTime();
        }
        influencePercentage = Mathf.Max(0, influencePercentage - influenceDecreaseRate * Time.deltaTime);
    }

    Color choseColor()
    {
        return Color.Lerp(baseColor, influencedColor, influencePercentage / 100f);
    }

    public void updateInfluence(float quantity)
    {
        Debug.Log("influence updated");
        influencePercentage = Mathf.Clamp(quantity + influencePercentage, 0, 100);
        Debug.Log(influencePercentage);

        SceneManager.instance.mediasUpdateInfluenceValue(influencePercentage);
    }

    protected override Vector3 generateDecalage()
    {
        return new Vector3(Random.Range(-xSpawnAmplitude, 0.1f), Random.Range(-ySpawnAmplitude, ySpawnAmplitude));
    }
}
