using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{

    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;


    private void OnEnable()
    {
        _restartButton.onClick.AddListener(RestartGame);
        _exitButton.onClick.AddListener(ExitGame);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(RestartGame);
        _exitButton.onClick.RemoveListener(ExitGame);
    }

    private void RestartGame()
    {
        SceneLoader.Instance.Load("BaseScene");
    }

    private void ExitGame()
    {
        Application.Quit();
    }

}
