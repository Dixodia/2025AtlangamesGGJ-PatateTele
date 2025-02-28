using System;
using System.Collections;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager instance;
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
    [SerializeField] SimpleBubbleManager lastWord;
    bool canGoNext = false;
    bool canEndGame = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        Debug.Log("awaken");
        instance = this;
        sceneNb -= 1;
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
                if (consecutiveRedMessages > consecutiveMsgGoal && !canGoNext) 
                {
                    CallFunctionAfterDelay(3, goToNextScene); 
                    canGoNext = true;
                }
                break;

            case 1:
                if (TVInfluence > 90 && !canGoNext) 
                {
                    CallFunctionAfterDelay(3, goToNextScene);
                    canGoNext = true;
                }
                break;
            case 2:
                if (disputeFriend.convIsStopped && disputeFriend.activeBubbles.Count == 0 && disputeGuy.activeBubbles.Count == 0 && !canGoNext)
                {
                    CallFunctionAfterDelay(2, spawnGreenBubble);
                    CallFunctionAfterDelay(5, goToNextScene);
                    canGoNext = true;
                }
                break;
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

        if (sceneNb == 3) CallFunctionAfterDelay(5, enableGoNext);
        if (sceneNb == 4)
        {
            CallFunctionAfterDelay(3, allowEndGame);
        }
    }

    public void endGame()
    {
        //show credits
        Debug.Log("Game Ended");
        SceneManager.LoadScene("Credit_Scene", LoadSceneMode.Single);
    }

    void enableGoNext()
    {
        canGoNext = true;
        Debug.Log("can go next");
    }

    void allowEndGame()
    {
        canEndGame = true;
    }

    // Start the coroutine to call FunctionToCall after a specified delay
    public void CallFunctionAfterDelay(float delay, Action func)
    {
        Debug.Log("call after delay");
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

    private void spawnGreenBubble()
    {
        lastWord.ShowNextBubble();
        lastWord.currentBubble.launchBubble(true);
        float newScale = 0.15f;
        lastWord.currentBubble.transform.localScale = new Vector3(newScale, newScale, 0);
    }
}
