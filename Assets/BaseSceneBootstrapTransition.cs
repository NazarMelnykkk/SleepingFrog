using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSceneBootstrapTransition : MonoBehaviour
{
    [Header("Scenes To Transition")]
    [SerializeField] private string _startTransitionScene = "LVL1Scene";

    private void Start()
    {
        SceneLoader.Instance.Add(_startTransitionScene);
    }
}
