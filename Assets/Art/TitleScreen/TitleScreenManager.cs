using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine.SceneManagement;


public class TitleScreenManager : MonoBehaviour
{
    [Header("Main Videos")]
    public VideoPlayer video1; // Première vidéo
    public VideoPlayer video2; // Deuxième vidéo
    public float delayBeforeFirstVideo = 2.0f; // Délai avant le lancement de la première vidéo
    public float delayBeforeSecondVideo = 5.0f; // Délai avant le lancement de la deuxième vidéo

    [Header("Button Video")]
    public VideoPlayer playButtonVideo; // Vidéo pour le bouton Play
       public Transform playButtonTransform; // Transform du bouton Play
    public float delayBeforePlayButton = 2.0f; // Délai avant l'apparition du bouton Play

    [Header("Background Video")]
    public VideoPlayer backgroundVideo; // Vidéo de fond qui joue en arrière-plan

    void Start()
    {

        // Désactiver les vidéos au départ
        video1.gameObject.SetActive(false);
        video2.gameObject.SetActive(false);
        playButtonVideo.gameObject.SetActive(false);
        backgroundVideo.gameObject.SetActive(false);

        // Démarrer le délai avant la première vidéo
        Invoke("PlayFirstVideo", delayBeforeFirstVideo);
    }



    void PlayFirstVideo()
    {
        if (video1)
        {
            video1.gameObject.SetActive(true);
            video1.Play();

            // Préparer la seconde vidéo après un délai
            Invoke("PlaySecondVideo", delayBeforeSecondVideo);
        }
    }

    void PlaySecondVideo()
    {
        if (video1)
        {
            video1.Stop();
            video1.gameObject.SetActive(false);
        }

        if (video2)
        {
            video2.gameObject.SetActive(true);
            video2.Play();

            // Préparer l'apparition du bouton Play après la deuxième vidéo
            Invoke("ActivatePlayButtonVideo", (float)video2.length + delayBeforePlayButton);
        }
    }

    void ActivatePlayButtonVideo()
    {
        if (playButtonVideo)
        {
            playButtonVideo.gameObject.SetActive(true);
            playButtonVideo.Play();

            // Activer les interactions sur le bouton Play
            if (playButtonTransform != null)
            {
                playButtonTransform.gameObject.SetActive(true);
            }
        }

        // Lancer la vidéo de fond
        if (backgroundVideo)
        {
            backgroundVideo.gameObject.SetActive(true);
            backgroundVideo.Play();
        }
    }

}
