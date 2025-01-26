using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadTitleScreen : MonoBehaviour
{
    public string titleScene;

    void Update()
    {
        // Vérifie si la touche Echap est enfoncée
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReloadTitleScene();
        }
    }

    void ReloadTitleScene()
    {
        // Charge la scène TitleScreen en mode Single (remplace l'ancienne scène)
        SceneManager.LoadScene(titleScene, LoadSceneMode.Single);

        // Décharge toutes les autres scènes actuellement chargées
    }
}
