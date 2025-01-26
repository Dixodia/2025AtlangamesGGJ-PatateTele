using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScriptsScene : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] string scriptsSceneName;
    void Start()
    {
        SceneManager.LoadScene(scriptsSceneName, LoadSceneMode.Additive);

    }
}
