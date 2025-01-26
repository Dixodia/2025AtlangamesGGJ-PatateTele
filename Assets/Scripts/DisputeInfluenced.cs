using UnityEngine;

public class DisputeInfluenced : BubbleManager
{
    [SerializeField] float maxSizeRatio;
    [SerializeField] float initScaleFactor;

    public void answer(float disputeNb, bool isDestructible)
    {
        ShowNextBubble();
        currentBubble.transform.localScale = initScaleFactor * currentBubble.transform.localScale * (1+disputeNb/100f* maxSizeRatio);
        currentBubble.transform.rotation = Quaternion.Euler(0.5f * new Vector3(0, 0, 3 * Random.Range(-disputeNb, disputeNb)));
        currentBubble.launchBubble(isDestructible);
    }

    protected override Vector3 generateDecalage()
    {
        return new Vector3(-xSpawnAmplitude, 0, 0);
    }
}
