using UnityEngine;

public class SimpleBubbleManager : BubbleManager
{
    protected override Vector3 generateDecalage()
    {
        return Vector3.left * 0.5f;
    }
}
