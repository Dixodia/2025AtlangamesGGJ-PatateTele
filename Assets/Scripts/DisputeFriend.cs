using System;
using System.Collections;
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
    public bool convCanUnPop = false;

    public float conversationIntensity = 0;

    [SerializeField] Color intenseColor = Color.red;

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
            currentColor = Color.Lerp(Color.white, intenseColor, conversationIntensity / intensityFireThreshold);
            ShowNextBubble();
            float newScale = initScaleFactor * currentBubble.transform.localScale.x * (1 + conversationIntensity / 40);
            currentBubble.transform.localScale = new Vector3(newScale, newScale, 1);
            currentBubble.transform.rotation = Quaternion.Euler(0.5f * new Vector3(0, 0, 3 * UnityEngine.Random.Range(-conversationIntensity, conversationIntensity)));
            currentBubble.launchBubble(intensityCantEraseThreshold > conversationIntensity);
            CallFunctionAfterDelay(0.3f, disputeGuy.answer, conversationIntensity, intensityCantEraseThreshold > conversationIntensity);
        }
    }

    public virtual void OnPointerDown(PointerEventData evData)
    {
        if(!convIsFire) launchBubbleWithSize(conversationIntensity);
    }

    public virtual void OnPointerClick(PointerEventData evData)
    {

    }


    protected float generateNextTime()
    {
        float nextTime = Time.realtimeSinceStartup + 6f/(conversationIntensity);
        return nextTime;
    }

    private void stopConv()
    {
        convIsStopped = true;
        

        CallFunctionAfterDelay(4f, enablePop, 0f, true);
    }

    protected override Vector3 generateDecalage()
    {
        return new Vector3(UnityEngine.Random.Range(0 ,xSpawnAmplitude), UnityEngine.Random.Range(0, ySpawnAmplitude), 0);
    }

    public void CallFunctionAfterDelay(float delay, Action<float, bool> func, float fl, bool bl)
    {
        Debug.Log("call after delay");
        StartCoroutine(CallAfterDelayCoroutine(delay, func, fl, bl));
    }

    // Coroutine to wait for 'delay' seconds and then call the target function
    private IEnumerator CallAfterDelayCoroutine(float delay, Action<float, bool> func, float fl, bool bl)
    {
        // Wait for the specified number of seconds
        yield return new WaitForSeconds(delay);

        // Call the function after the delay
        func?.Invoke(fl, bl);
    }

    private void enablePop(float f, bool b)
    {
        foreach (Bubble bubble in activeBubbles)
        {
            bubble.AddComponent<ClickToDestroy>();
            bubble.AddComponent<BoxCollider>();
        }

        foreach (Bubble bubble in disputeGuy.activeBubbles)
        {
            bubble.AddComponent<ClickToDestroy>();
            bubble.AddComponent<BoxCollider>();
        }

        //change cursor
    }
}
