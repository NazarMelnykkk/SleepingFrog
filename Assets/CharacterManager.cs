using System.Collections;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public Agent Character;
    public AgentConfigBase AgentConfigBase;

    private float delay = 1f;

    private void Awake()
    {
        CharacterInit();
    }
    private void CharacterInit()
    {
        Character.Init(AgentConfigBase);
    }

    private void Start()
    {
        Character.OnDieEvent += EndGame;
    }

    private void OnDisable()
    {
        //Character.OnDieEvent -= EndGame;
    }

    private void EndGame()
    {
        SceneLoader.Instance.Load("MenuScene");
    }

    private IEnumerator GameEndDelay()
    {
        yield return new WaitForSeconds(delay);

        SceneLoader.Instance.Load("MenuScene");

    }
}
