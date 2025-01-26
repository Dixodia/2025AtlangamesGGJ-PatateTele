using UnityEngine;

public class MusicManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public AudioSource audioSource;  // Reference to the AudioSource component
    public AudioSource audioSourceLoop;  // Reference to the AudioSource component

    bool hasStarted = false;
    private void Start()
    {
        audioSource.Play();
    }

    void Update()
    {
        // Check if the AudioSource is playing and if it has finished the clip
        if (!audioSource.isPlaying && !hasStarted)
        {
            Debug.Log("Audio has finished playing.");
            hasStarted = true;
            audioSourceLoop.Play();
        }
    }
    
}
