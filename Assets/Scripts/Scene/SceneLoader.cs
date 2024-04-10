using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    [SerializeField] private string _systemScene = "SystemScene";
    [SerializeField] private string _menuScene = "MenuScene";
    [SerializeField] private string _baseScene = "BaseScene";
    [SerializeField] private string _lvl1Scene = "_LVL1Scene";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Load(_baseScene);
    }

    public void Load(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);

        UnloadAllPlayScenes(sceneToLoad);
    }

    public void Add(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);
    }

    public void Transition(string sceneToLoad, string sceneToUnload)
    {
        SceneManager.UnloadSceneAsync(sceneToUnload);
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);
    }

    public void UnloadAllPlayScenes(string sceneException)
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name != _systemScene && scene.name != sceneException)
            {
                SceneManager.UnloadSceneAsync(scene);
            }
        }
    }

    public void TransitionToGameScene(string newGameSceneName)
    {
        UnloadGameScene();

        SceneManager.LoadScene(newGameSceneName, LoadSceneMode.Additive);
    }

    private void UnloadGameScene()
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name != _baseScene && scene.name != _systemScene)
            {
                SceneManager.UnloadSceneAsync(scene);
            }
        }
    }
}

