using UnityEngine;

public class TVInfluencedFriend : ConstantChatBubbleManager
{
    [SerializeField] Color baseColor;
    [SerializeField] Color influencedColor;

    public float influencePercentage = 0;
    [SerializeField] float influenceDecreaseRate;

    protected override void Update()
    {
        if (Time.realtimeSinceStartup > nextBubbleTimer)
        {
            currentColor = choseColor();
            ShowNextBubble();
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
}
