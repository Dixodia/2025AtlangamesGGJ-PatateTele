using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class FadeInEffect : MonoBehaviour
{
    public CanvasGroup blackFade; // R�f�rence au CanvasGroup qui contr�le le fondu
    public float fadeDuration = 2f; // Dur�e du fondu (en secondes)
    
    void Start()
    {
        // D�marre le fondu au noir d�s que la sc�ne commence
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(3f);

        float elapsedTime = 0f;

        // Initialement, le CanvasGroup a une alpha de 1 (noir complet)
        blackFade.alpha = 1f;

        while (elapsedTime < fadeDuration)
        {
            // Progressivement r�duire l'alpha � 0 (effet de fondu)
            blackFade.alpha = 1f - (elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Assurez-vous que l'alpha est exactement 0 � la fin du fondu
        blackFade.alpha = 0f;
    }
}
