using UnityEngine;
using static Unity.VisualScripting.Member;

public class SpectatorManager : ConstantChatBubbleManager
{
    [SerializeField] Color baseColor;
    [SerializeField] Color influencedColor;

    float influencePercentage;
    [SerializeField] float influenceDecreaseRate;
    AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
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
            currentBubble.launchBubble(true);
            nextBubbleTimer = generateNextTime();
            SceneManager.instance.politicianUpdateConsecutive(currentColor == influencedColor);
        }
        influencePercentage = Mathf.Max(0, influencePercentage - influenceDecreaseRate * Time.deltaTime);
    }

    public void updateInfluence(float quantity)
    {
        Debug.Log("influence updated");
        influencePercentage = Mathf.Clamp(quantity + influencePercentage, 0, 100);
    }

    protected override float generateNextTime()
    {
        float nextTime = Time.realtimeSinceStartup + minBubblePeriod * (1-influencePercentage / 100f * 0.95f) ;
        return nextTime;
    }

    private void OnEnable()
    {
        source.Play();
    }

    private void OnDisable()
    {
        if (source != null) source.Stop();
    }
}
