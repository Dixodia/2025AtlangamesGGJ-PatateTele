using UnityEngine;
using UnityEngine.EventSystems;

public class OnClickBubbleManager : BubbleManager, IPointerDownHandler
{
    public float upscaleSpeed;
    public float maxScale;

    protected override void Update()
    {
        base.Update();
        //Debug.Log(currentBubble);
        //Debug.Log(currentBubble.launched);
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
            }
            else
            {
                currentBubble.launchBubble();
            }
        }
    }

    public virtual void OnPointerDown(PointerEventData evData)
    {
        ShowNextBubble();
    }
}
