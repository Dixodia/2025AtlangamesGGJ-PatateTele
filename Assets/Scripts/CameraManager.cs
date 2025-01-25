using NUnit.Framework;
using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    
    [SerializeField] CinemachineCamera[] cameras;

    int camIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        cameras[0].enabled = true;
        for (int i = 1; i < cameras.Length; i++)
        {
            cameras[i].enabled = false;
        }
    }

    public void nextCam()
    {
        cameras[camIndex+1].enabled = true;
        cameras[camIndex].enabled = false;
        camIndex++;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("space");
            nextCam();
        }
    }
}
