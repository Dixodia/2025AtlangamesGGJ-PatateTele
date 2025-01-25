using UnityEngine;
using UnityEngine.EventSystems;

public class PickableColor : MonoBehaviour, IPointerClickHandler
{
    public Color myColor;
    public BubbleManager bubbleManager;
    public void OnPointerClick(PointerEventData evData)
    {
        bubbleManager.setColor(myColor);
        //Debug.Log("Picked " + myColor.ToString());
    }
}