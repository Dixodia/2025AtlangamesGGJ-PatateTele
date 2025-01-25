using UnityEngine;
using UnityEngine.EventSystems;

public class Politician : OnClickBubbleManager
{
    [SerializeField] SpectatorManager spectator;
    [SerializeField] float xBaseDecalage;

    public override void OnPointerDown(PointerEventData evData)
    {
        base.OnPointerDown(evData);
        if (Color.red == currentColor) spectator.updateInfluence(10);
    }

    protected override Vector3 generateDecalage()
    {
        float randNb = Random.Range(-1, 1);
        if(randNb<0) randNb = -1; else randNb = 1;
        return new Vector3(Random.Range(-xSpawnAmplitude, xSpawnAmplitude) + xBaseDecalage * randNb, Random.Range(-ySpawnAmplitude, ySpawnAmplitude));
    }
}
