using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using DG.Tweening; // Nécessaire pour DoTween
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PlayButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
        [Header("Scenes to Load")]
#if UNITY_EDITOR
    public SceneAsset worldScene; // Drag and drop the 3D world scene here
    public SceneAsset scriptsScene; // Drag and drop the scripts scene here
    public SceneAsset titleScene;
#endif

    [SerializeField] private string worldSceneName;
    private string titleSceneName;
    private string scriptsSceneName;

        [Header("Play Button")]
    public Transform playButtonTransform; // Transform du bouton Play
    public float hoverScale = 1.2f; // Échelle quand on survole le bouton
    private Vector3 currentScale = new Vector3(0f, 0f, 0f); // Échelle quand on survole le bouton
    public float clickScale = 1.3f; // Échelle lors du clic
    public float hoverDuration = 0.2f; // Durée de l’animation au survol
    public float clickDuration = 0.5f; // Durée de l’animation au clic
    public string playButtonTag = "Player"; // Tag pour identifier le bouton Play

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentScale = playButtonTransform.localScale;// Récupère les noms des scènes depuis les assets

#if UNITY_EDITOR

        if (scriptsScene != null)
        {
            scriptsSceneName = scriptsScene.name;
        }

        if (titleScene != null)
        {
            titleSceneName = titleScene.name;
        }
#endif
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadGameScenes();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
            // Vérifiez si l'objet que le pointeur survole a le tag PlayButton
        if (eventData.pointerEnter.CompareTag(playButtonTag))
        {
            // Appeler la méthode de l'animation de survol
            
            OnPlayButtonHoverEnter();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
            // Appeler la méthode de l'animation de survol
            OnPlayButtonHoverExit();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
            // Vérifiez si l'objet que le pointeur survole a le tag PlayButton
        if (eventData.pointerEnter.CompareTag(playButtonTag))
        {
            LoadGameScenes();
        }
    }
    
    public void OnPlayButtonHoverEnter()
    {
        if (playButtonTransform != null)
        {
            playButtonTransform.DOScale(currentScale*hoverScale, hoverDuration).SetEase(Ease.OutQuad);
        }
    }

    public void OnPlayButtonHoverExit()
    {
        if (playButtonTransform != null)
        {
            playButtonTransform.DOScale(currentScale*1.0f, hoverDuration).SetEase(Ease.OutQuad);
        }
    }

    void LoadGameScenes()
    {
        SceneManager.LoadScene("GAME_ART", LoadSceneMode.Single);
    }
}
