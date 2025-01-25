using UnityEngine;

public class ConstantChatBubbleManager : BubbleManager
{
    [SerializeField] float minBubblePeriod;
    [SerializeField] float bubblePeriodAmplitude;
    [SerializeField] float nextBubbleTimer;

    [SerializeField] Color baseColor;
    [SerializeField] Color influencedColor;

    float influencePercentage;
    [SerializeField] float influenceDecreaseRate;

    protected override void Start()
    {
        base.Start();
        nextBubbleTimer = generateNextTime();
    }

    protected override void Update()
    {
        if (Time.realtimeSinceStartup > nextBubbleTimer)
        {
            float randNb = Random.value * 100;
            if (randNb < influencePercentage)
            {
                currentColor = influencedColor;
            }
            else
            {
                currentColor = baseColor;
            }
            ShowNextBubble();
            currentBubble.launchBubble();
            nextBubbleTimer = generateNextTime();
        }

        influencePercentage = Mathf.Max(0, influencePercentage - influenceDecreaseRate * Time.deltaTime);
    }

    float generateNextTime()
    {
        float nextTime = Time.realtimeSinceStartup + minBubblePeriod + Random.Range(0, bubblePeriodAmplitude);
        return nextTime;
    }

    public void updateInfluence(float quantity)
    {
        Debug.Log("influence updated");
        influencePercentage = Mathf.Clamp(quantity + influencePercentage, 0, 100);
    }
}
