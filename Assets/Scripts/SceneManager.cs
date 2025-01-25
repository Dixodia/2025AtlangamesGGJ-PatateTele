using UnityEngine;

public class SceneManager : MonoBehaviour
{
    int sceneNb = 0;

    //Scene 0
    int consecutiveRedMessages = 0;
    [SerializeField] int consecutiveMsgGoal;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void checkCondition()
    {
        switch (sceneNb)
        {
            case 0:
                if(consecutiveRedMessages > )
            default:
                break;
        }
    }

    public void politicianUpdateConsecutive(bool isRed)
    {
        if (isRed) consecutiveRedMessages++;
        else consecutiveRedMessages = 0;
    }
}
