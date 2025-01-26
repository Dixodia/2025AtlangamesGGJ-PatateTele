using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine.SceneManagement;


public class TitleScreenManager : MonoBehaviour
{
    [Header("Main Videos")]
    public VideoPlayer video1; // Premi�re vid�o
    public VideoPlayer video2; // Deuxi�me vid�o
    public float delayBeforeFirstVideo = 2.0f; // D�lai avant le lancement de la premi�re vid�o
    public float delayBeforeSecondVideo = 5.0f; // D�lai avant le lancement de la deuxi�me vid�o

    [Header("Button Video")]
    public VideoPlayer playButtonVideo; // Vid�o pour le bouton Play
       public Transform playButtonTransform; // Transform du bouton Play
    public float delayBeforePlayButton = 2.0f; // D�lai avant l'apparition du bouton Play

    [Header("Background Video")]
    public VideoPlayer backgroundVideo; // Vid�o de fond qui joue en arri�re-plan

    void Start()
    {

        // D�sactiver les vid�os au d�part
        video1.gameObject.SetActive(false);
        video2.gameObject.SetActive(false);
        playButtonVideo.gameObject.SetActive(false);
        backgroundVideo.gameObject.SetActive(false);

        // D�marrer le d�lai avant la premi�re vid�o
        Invoke("PlayFirstVideo", delayBeforeFirstVideo);
    }



    void PlayFirstVideo()
    {
        if (video1)
        {
            video1.gameObject.SetActive(true);
            video1.Play();

            // Pr�parer la seconde vid�o apr�s un d�lai
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

            // Pr�parer l'apparition du bouton Play apr�s la deuxi�me vid�o
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

        // Lancer la vid�o de fond
        if (backgroundVideo)
        {
            backgroundVideo.gameObject.SetActive(true);
            backgroundVideo.Play();
        }
    }

}
