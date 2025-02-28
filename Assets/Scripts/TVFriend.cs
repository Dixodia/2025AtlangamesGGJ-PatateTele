using UnityEngine;

public class TVFriend : OnClickBubbleManager
{
    [SerializeField] float influenceNb;
    [SerializeField] TVInfluencedFriend InfluencedFriend;
    [SerializeField] float minSizeThreshold;

    float initScale = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

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
                float endScale = currentBubble.launchBubble(true);
                Debug.Log(endScale / initScale);

                if(endScale / initScale > minSizeThreshold) InfluencedFriend.updateInfluence(influenceNb * (endScale / initScale));
            }
        }
    }

    protected override Vector3 generateDecalage()
    {
        return new Vector3(Random.Range(0.1f, xSpawnAmplitude), Random.Range(-ySpawnAmplitude, ySpawnAmplitude));
    }

    protected override void storeBubbleValues(GameObject bubbleObj)
    {
        base.storeBubbleValues(bubbleObj);
        initScale = bubbleObj.transform.localScale.x;
    }
}
