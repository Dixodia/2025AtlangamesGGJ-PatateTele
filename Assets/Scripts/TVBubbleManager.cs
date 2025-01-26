using UnityEngine;

public class TVBubbleManager : ConstantChatBubbleManager
{
    [SerializeField] float influenceNb;
    [SerializeField] TVInfluencedFriend InfluencedFriend;
    [SerializeField] float maxScaleOverTime;
    float timeFactor = 1.0f;

    float initScale;


    [SerializeField] ScreenShake screenShake;

    AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    protected override void Update()
    {
        if (Time.realtimeSinceStartup > nextBubbleTimer)
        {
            ShowNextBubble();
            float scale = initScale * 1.3f * Mathf.Clamp( 1 + timeFactor / 2f, 1, maxScaleOverTime);
            currentBubble.transform.localScale = new Vector3(scale, scale, 1);
            currentBubble.launchBubble(true);
            nextBubbleTimer = generateNextTime();
            InfluencedFriend.updateInfluence(influenceNb + timeFactor * 1.5f);
            screenShake.ShakeCamera(timeFactor /10 * 0.01f + 0.01f, timeFactor * 0.01f, 0.05f);
        }
        timeFactor += Time.deltaTime / 3;
    }

    protected override void storeBubbleValues(GameObject bubbleObj)
    {
        base.storeBubbleValues(bubbleObj);
        initScale = bubbleObj.transform.localScale.x;
        bubbleObj.GetComponent<SpriteRenderer>().flipX = false;
    }

    private void OnEnable()
    {
        source.Play();
    }

    private void OnDisable()
    {
        if(source!= null) source.Stop();
    }
}