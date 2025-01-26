using System;
using System.Collections;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public static SceneManager instance;
    [SerializeField] GameObject[] scenePackages;

    [SerializeField] int sceneNb;

    //Scene 1
    int consecutiveRedMessages = 0;
    [SerializeField] int consecutiveMsgGoal;

    //Scene 2
    float TVInfluence = 0;

    //Scene3
    [SerializeField] DisputeInfluenced disputeGuy;
    [SerializeField] DisputeFriend disputeFriend;

    bool canGoNext = false;
    bool canEndGame = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        sceneNb -= 1;
        instance = this;
        for (int i = 0; i < scenePackages.Length; i++)
        {
            foreach (BubbleManager manager in scenePackages[i].GetComponentsInChildren<BubbleManager>())
            {
                manager.enabled = false;
            }
        }

        foreach (BubbleManager manager in scenePackages[sceneNb].GetComponentsInChildren<BubbleManager>())
        {
            manager.enabled = true;
        }
    }
    private void Start()
    {
        CameraManager.instance.init(sceneNb);
    }

    // Update is called once per frame
    void Update()
    {
        checkCondition();

        if(canEndGame && Input.GetMouseButtonDown(0)) endGame();
    }

    void checkCondition()
    {
        switch (sceneNb)
        {
            case 0:
                if (consecutiveRedMessages > consecutiveMsgGoal) goToNextScene(); break;
            case 1:
                if (TVInfluence > 90) goToNextScene(); break;
            case 2:
                if (disputeFriend.convIsStopped && disputeFriend.activeBubbles.Count == 0 && disputeGuy.activeBubbles.Count == 0) goToNextScene(); break;
            case 3:
                if(canGoNext) goToNextScene(); break;
            default:
                break;
        }
    }

    public void politicianUpdateConsecutive(bool isRed)
    {
        if (isRed) consecutiveRedMessages++;
        else consecutiveRedMessages = 0;
        Debug.Log(consecutiveRedMessages);
    }

    public void mediasUpdateInfluenceValue(float currentValue)
    {
        TVInfluence = currentValue;
    }

    private void goToNextScene()
    {

        canGoNext = false;

        CameraManager.instance.nextCam();

        foreach (BubbleManager manager in scenePackages[sceneNb].GetComponentsInChildren<BubbleManager>())
        {
            manager.enabled = false;
        }


        sceneNb++;
        foreach (BubbleManager manager in scenePackages[sceneNb].GetComponentsInChildren<BubbleManager>())
        {
            manager.enabled = true;
        }

        if (sceneNb == 4) CallFunctionAfterDelay(3, enableGoNext);
        if (sceneNb == 5)
        {
            CallFunctionAfterDelay(3, allowEndGame);
        }
    }

    public void endGame()
    {
        //show credits
        Debug.Log("Game Ended");
    }

    void enableGoNext()
    {
        canGoNext = true;
    }

    void allowEndGame()
    {
        canEndGame = true;
    }

    // Start the coroutine to call FunctionToCall after a specified delay
    public void CallFunctionAfterDelay(float delay, Action func)
    {
        StartCoroutine(CallAfterDelayCoroutine(delay, func));
    }

    // Coroutine to wait for 'delay' seconds and then call the target function
    private IEnumerator CallAfterDelayCoroutine(float delay, Action func)
    {
        // Wait for the specified number of seconds
        yield return new WaitForSeconds(delay);

        // Call the function after the delay
        func?.Invoke();
    }
}
