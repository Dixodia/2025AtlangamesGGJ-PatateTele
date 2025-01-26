using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadTitleScreen : MonoBehaviour
{
    public string titleScene;

    void Update()
    {
        // V�rifie si la touche Echap est enfonc�e
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReloadTitleScene();
        }
    }

    void ReloadTitleScene()
    {
        // Charge la sc�ne TitleScreen en mode Single (remplace l'ancienne sc�ne)
        SceneManager.LoadScene(titleScene, LoadSceneMode.Single);

        // D�charge toutes les autres sc�nes actuellement charg�es
    }
}
