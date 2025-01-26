using UnityEngine;
using UnityEngine.EventSystems;

public class ClickToDestroy : MonoBehaviour, IPointerClickHandler
{
    public virtual void OnPointerClick(PointerEventData evData)
    {
        GetComponent<Bubble>().DestroyBubble();
    }

}
