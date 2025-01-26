using UnityEngine;

public class SimpleBubbleManager : BubbleManager
{
    protected override Vector3 generateDecalage()
    {
        return Vector3.right * 0.5f;
    }
}
