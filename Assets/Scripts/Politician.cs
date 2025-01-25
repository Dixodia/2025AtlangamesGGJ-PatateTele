using UnityEngine;
using UnityEngine.EventSystems;

public class Politician : OnClickBubbleManager
{
    [SerializeField] ConstantChatBubbleManager spectator;

    public override void OnPointerDown(PointerEventData evData)
    {
        base.OnPointerDown(evData);
        if (Color.red.r - currentColor.r < 0.3) spectator.updateInfluence(10);
    }
}
