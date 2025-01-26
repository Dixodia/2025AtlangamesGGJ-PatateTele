using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class FadeInEffect : MonoBehaviour
{
    public CanvasGroup blackFade; // Référence au CanvasGroup qui contrôle le fondu
    public float fadeDuration = 2f; // Durée du fondu (en secondes)
    
    void Start()
    {
        // Démarre le fondu au noir dès que la scène commence
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
            // Progressivement réduire l'alpha à 0 (effet de fondu)
            blackFade.alpha = 1f - (elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Assurez-vous que l'alpha est exactement 0 à la fin du fondu
        blackFade.alpha = 0f;
    }
}
