using UnityEngine;
using UnityEngine.EventSystems;

public class Politician : OnClickBubbleManager
{
    [SerializeField] SpectatorManager spectator;
    [SerializeField] float xBaseDecalage;
    int colorNb = 0;
    //[ColorUsageAttribute(false, false)]
    [SerializeField] Color[] colorSet;

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

    protected override void Update()
    {
        base.Update();
        if (Input.GetMouseButtonDown(1))
        {
            colorNb += 1;
            if (colorNb >= colorSet.Length) colorNb = 0;
            currentColor = colorSet[colorNb];
        }
    }
}
