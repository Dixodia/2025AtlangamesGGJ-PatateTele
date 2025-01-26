using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    // The maximum number of audio sources to handle multiple simultaneous sounds
    public int maxAudioSources = 10;

    // List to hold available audio sources
    [SerializeField] public List<AudioSource> audioSources;

    // A dictionary to hold the audio clips by name for easy access
    [SerializeField] public List<AudioClip> audioClips;

    //0 bubble pop

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        audioSources = new List<AudioSource>();

        // Preallocate a pool of audio sources to handle multiple sounds
        for (int i = 0; i < maxAudioSources; i++)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.playOnAwake = false;  // Don't play the sound immediately when instantiated
            audioSources.Add(source);
        }
    }



    public AudioSource PlaySoundFromInt(int clipNb)
    {
        if (audioClips.Count > clipNb)
        {
            return PlaySound(audioClips[clipNb]);
        }
        else
        {
            Debug.LogWarning("Sound not found in dictionary");
        }
        return null;
    }

    // Play a sound by directly passing the AudioClip
    public AudioSource PlaySound(AudioClip clip)
    {
        AudioSource source = GetAvailableAudioSource();
        Debug.Log("sound played");
        if (source != null)
        {
            source.clip = clip;
            source.volume = 1.0f;
            source.Play();
        }
        else
        {
            Debug.LogWarning("No available audio sources!");
        }
        return source;
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
