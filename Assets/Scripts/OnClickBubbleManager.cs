using UnityEngine;
using UnityEngine.EventSystems;

public class OnClickBubbleManager : BubbleManager, IPointerDownHandler
{
    public float upscaleSpeed;
    public float maxScale;

    protected override void Update()
    {
        base.Update();
        if (currentBubble != null && !currentBubble.launched)
        {
            if (Input.GetMouseButton(0) && currentBubble.transform.localScale.x < maxScale)
            {
                float newScale = Mathf.Min(maxScale, currentBubble.transform.localScale.x + upscaleSpeed * Time.deltaTime);
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
        Debug.Log("pop");
        ShowNextBubble();
    }
}
