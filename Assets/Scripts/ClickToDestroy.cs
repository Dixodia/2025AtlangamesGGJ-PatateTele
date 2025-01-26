using UnityEngine;
using UnityEngine.EventSystems;

public class ClickToDestroy : MonoBehaviour, IPointerClickHandler
{
    public virtual void OnPointerClick(PointerEventData evData)
    {
        ParticleEffectManager.instance.PlayParticleEffect(transform.Find("ParticleCenter"), ParticleEffectManager.instance.particleList[0]);
        GetComponent<Bubble>().DestroyBubble();
    }

    
}
