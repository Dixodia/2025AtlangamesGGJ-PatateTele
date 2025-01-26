using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DisputeFriend : BubbleManager, IPointerDownHandler
{
    [SerializeField] DisputeInfluenced disputeGuy;

    [SerializeField] int intensityStartAuto;
    [SerializeField] float intensityDescalationSpeed;
    [SerializeField] protected float nextBubbleTimer;
    [SerializeField] float intensityCalmThreshold; //Itensity doesn't go down anymore
    [SerializeField] float intensityFireThreshold; //Talking is now automatic
    [SerializeField] float intensityCantEraseThreshold; //NewBubblesDon'tDisappear
    [SerializeField] float intensityStopThreshold; //No talking anymore

    [SerializeField] float initScaleFactor;

    bool converIsCalm = true;
    bool convIsFire = false;
    public bool convIsStopped = false;

    public float conversationIntensity = 0;

    protected override void Update()
    {
        base.Update();
        if(conversationIntensity > intensityStartAuto && nextBubbleTimer < Time.realtimeSinceStartup && !convIsStopped)
        {
            launchBubbleWithSize(conversationIntensity);
            nextBubbleTimer = generateNextTime();
        }


        if (converIsCalm)
        {
            if(conversationIntensity > intensityCalmThreshold) converIsCalm = false;
            conversationIntensity = Mathf.Max(0, conversationIntensity - Time.deltaTime * intensityDescalationSpeed);
        }

        if (conversationIntensity > intensityFireThreshold)
        {
            convIsFire = true;
        }

        
        if (!convIsStopped && conversationIntensity > intensityStopThreshold) stopConv();
    }

    private void launchBubbleWithSize(float intensity)
    {
        if (!convIsStopped)
        {
            conversationIntensity += 1;
            ShowNextBubble();
            float newScale = initScaleFactor * currentBubble.transform.localScale.x * (1 + conversationIntensity / 40);
            currentBubble.transform.localScale = new Vector3(newScale, newScale, 1);
            currentBubble.transform.rotation = Quaternion.Euler(0.5f * new Vector3(0, 0, Random.Range(-conversationIntensity, conversationIntensity)));
            currentBubble.launchBubble(intensityCantEraseThreshold > conversationIntensity);
            disputeGuy.answer(conversationIntensity, intensityCantEraseThreshold > conversationIntensity);
        }
    }

    public virtual void OnPointerDown(PointerEventData evData)
    {
        if(!convIsFire) launchBubbleWithSize(conversationIntensity);
    }

    protected float generateNextTime()
    {
        float nextTime = Time.realtimeSinceStartup + 6f/(conversationIntensity);
        return nextTime;
    }

    private void stopConv()
    {
        convIsStopped = true;
        foreach(Bubble bubble in activeBubbles)
        {
            bubble.AddComponent<ClickToDestroy>();
            bubble.AddComponent<BoxCollider>();
        }

        foreach (Bubble bubble in disputeGuy.activeBubbles)
        {
            bubble.AddComponent<ClickToDestroy>();
            bubble.AddComponent<BoxCollider>();
        }
    }

    protected override Vector3 generateDecalage()
    {
        return new Vector3(Random.Range(0, xSpawnAmplitude), Random.Range(0, ySpawnAmplitude), 0);
    }
}
