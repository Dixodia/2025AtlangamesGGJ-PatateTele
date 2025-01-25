using UnityEngine;

public class DisputeFriend : OnClickBubbleManager
{
    [SerializeField] DisputeInfluenced disputeGuy;
    float conversationIntensity = 0;

    protected override void launchBehaviour()
    {
        if (currentBubble != null && !currentBubble.launched)
        {
            if (Input.GetMouseButton(0) && currentBubble.transform.lossyScale.x < maxScale)
            {
                float newScale = currentBubble.transform.localScale.x + upscaleSpeed * Time.deltaTime;
                currentBubble.transform.localScale = new Vector3(newScale, newScale, 1);
            }
            else
            {
                float endScale = currentBubble.launchBubble();
                disputeGuy.answer();

            }
        }
    }

    protected override void Update()
    {
        base.Update();
    }
}
