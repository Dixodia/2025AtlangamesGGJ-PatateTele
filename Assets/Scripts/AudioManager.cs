using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    // The maximum number of audio sources to handle multiple simultaneous sounds
    public int maxAudioSources = 10;

    // List to hold available audio sources
    [SerializeField] private List<AudioSource> audioSources;

    // A dictionary to hold the audio clips by name for easy access
    [SerializeField] public Dictionary<string, AudioClip> audioClips;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        audioSources = new List<AudioSource>();
        audioClips = new Dictionary<string, AudioClip>();

        // Preallocate a pool of audio sources to handle multiple sounds
        for (int i = 0; i < maxAudioSources; i++)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.playOnAwake = false;  // Don't play the sound immediately when instantiated
            audioSources.Add(source);
        }
    }

    // Load an audio clip into the dictionary for later use (assign this in the inspector or dynamically)
    public void LoadAudioClip(string clipName, AudioClip clip)
    {
        if (!audioClips.ContainsKey(clipName))
        {
            audioClips.Add(clipName, clip);
        }
        else
        {
            Debug.LogWarning("Audio clip with name " + clipName + " already exists!");
        }
    }

    // Play a sound by its name (e.g., from the dictionary)
    public void PlaySound(string clipName)
    {
        if (audioClips.ContainsKey(clipName))
        {
            PlaySound(audioClips[clipName]);
        }
        else
        {
            Debug.LogWarning("Sound not found in dictionary: " + clipName);
        }
    }

    // Play a sound by directly passing the AudioClip
    public void PlaySound(AudioClip clip)
    {
        AudioSource source = GetAvailableAudioSource();

        if (source != null)
        {
            source.clip = clip;
            source.Play();
        }
        else
        {
            Debug.LogWarning("No available audio sources!");
        }
    }

    // Stop all sounds
    public void StopAllSounds()
    {
        foreach (var source in audioSources)
        {
            source.Stop();
        }
    }

    // Get an available audio source that is not currently playing
    private AudioSource GetAvailableAudioSource()
    {
        foreach (var source in audioSources)
        {
            if (!source.isPlaying)
            {
                return source;
            }
        }
        return null; // No available audio sources
    }
}
