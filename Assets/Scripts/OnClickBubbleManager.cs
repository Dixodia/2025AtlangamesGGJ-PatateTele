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
        if (currentBubble != null && !currentBubble.launched)
        {
            Debug.Log("aaaaaaaaaaaaaaaaa");
            Debug.Log(Input.GetMouseButton(0));
            Debug.Log(currentBubble.transform.localScale.x);
            Debug.Log( maxScale);

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
        Debug.Log("pop");
        ShowNextBubble();
    }
}
