using UnityEngine;

public class ConstantChatBubbleManager : BubbleManager
{
    [SerializeField] protected float minBubblePeriod;
    [SerializeField] protected float bubblePeriodAmplitude;
    [SerializeField] protected float nextBubbleTimer;
    [SerializeField] protected Color initColor;
    protected override void Start()
    {
        base.Start();
        currentColor = initColor;
        nextBubbleTimer = generateNextTime();
    }

    protected override void Update()
    {
        if (Time.realtimeSinceStartup > nextBubbleTimer)
        {
            ShowNextBubble();
            currentBubble.launchBubble(true);
            nextBubbleTimer = generateNextTime();
        }
    }

    protected float generateNextTime()
    {
        float nextTime = Time.realtimeSinceStartup + minBubblePeriod + Random.Range(0, bubblePeriodAmplitude);
        return nextTime;
    }
}
