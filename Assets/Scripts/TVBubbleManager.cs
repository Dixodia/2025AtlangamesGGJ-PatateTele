using UnityEngine;

public class TVBubbleManager : ConstantChatBubbleManager
{
    [SerializeField] float influenceNb;
    [SerializeField] TVInfluencedFriend InfluencedFriend;
    [SerializeField] float maxScaleOverTime;
    float timeFactor = 1.0f;

    float initScale;
    protected override void Update()
    {
        if (Time.realtimeSinceStartup > nextBubbleTimer)
        {
            ShowNextBubble();
            float scale = initScale * Mathf.Clamp( 1 + timeFactor / 3f, 1, maxScaleOverTime);
            currentBubble.transform.localScale = new Vector3(scale, scale, 1);
            currentBubble.launchBubble();
            nextBubbleTimer = generateNextTime();
            InfluencedFriend.updateInfluence(influenceNb + timeFactor);
        }
        timeFactor += Time.deltaTime / 3;
    }

    protected override void storeBubbleValues(GameObject bubbleObj)
    {
        base.storeBubbleValues(bubbleObj);
        initScale = bubbleObj.transform.localScale.x;
        bubbleObj.GetComponent<SpriteRenderer>().flipX = false;
    }
}