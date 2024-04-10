using UnityEngine;

public class GameEventHandler : MonoBehaviour
{
    public static GameEventHandler Instance { get; private set; }

    public MiscEvents MiscEvents;

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

        MiscEvents = new MiscEvents();
    }
}
