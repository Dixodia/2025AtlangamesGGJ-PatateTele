using UnityEngine;
using UnityEngine.EventSystems;

public class PickableColor : MonoBehaviour, IPointerClickHandler
{
    public Color myColor;

    public void OnPointerClick(PointerEventData evData)
    {
        transform.parent.GetComponent<BubbleManager>().setColor(myColor);
        //Debug.Log("Picked " + myColor.ToString());
    }
}