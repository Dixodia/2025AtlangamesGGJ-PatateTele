using UnityEngine;
using UnityEngine.EventSystems;

public class OnClickBubbleManager : BubbleManager, IPointerDownHandler
{
    public float upscaleSpeed;
    public float maxScale;

    bool isInflating = false;
    AudioSource currentAudio;
    protected override void Update()
    {
        base.Update();
        launchBehaviour();
    }

    protected virtual void launchBehaviour()
    {
        if (currentBubble != null && !currentBubble.launched)
        {
            
            if (Input.GetMouseButton(0) && currentBubble.transform.lossyScale.x < maxScale)
            {
                float newScale = currentBubble.transform.localScale.x + upscaleSpeed * Time.deltaTime;
                currentBubble.transform.localScale = new Vector3(newScale, newScale, 1);
                if (!isInflating)
                {
                    isInflating = true;
                    currentAudio = AudioManager.instance.PlaySoundFromInt(2);
                }
            }
            else
            {
                currentAudio.Stop();
                currentBubble.launchBubble(true);
                isInflating=false;
            }
        }
    }

    public virtual void OnPointerDown(PointerEventData evData)
    {
        ShowNextBubble();
    }
}
