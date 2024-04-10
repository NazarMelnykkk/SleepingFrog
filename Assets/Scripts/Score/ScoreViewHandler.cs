using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreViewHandler : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _scoreText;
    private int _scoreValue;

    [Header("Animation Config")]
    [SerializeField] private Vector2 _maxAnimatedSize = new Vector2(2f, 2f);
    [SerializeField] private float _animationDuration = 1f;
    [SerializeField] private float _idleTimeBeforeShrink = 3f;

    private Vector2 _defaultSize;
    private Coroutine _sizeChangeCoroutine;

    private void OnEnable()
    {
        GameEventHandler.Instance.MiscEvents.OnEnemyDeathEvent += AddScore;
    }

    private void OnDisable()
    {
        GameEventHandler.Instance.MiscEvents.OnEnemyDeathEvent -= AddScore;
    }

    private void Start()
    {
        _defaultSize = _scoreText.gameObject.transform.localScale;

        ScoreStorage.ReseScore();
    }

    public void AddScore()
    {
        _scoreValue++;
        ScoreStorage.SetScore(_scoreValue);
        _scoreText.text = $"{_scoreValue}!";
        ScoreAnimatedEffect();
    }

    private void ScoreAnimatedEffect()
    {
        if (_sizeChangeCoroutine != null)
        {
            StopCoroutine(_sizeChangeCoroutine);
        }
        _sizeChangeCoroutine = StartCoroutine(ChangeTextSize(_maxAnimatedSize, _defaultSize, _animationDuration));
    }

    private IEnumerator ChangeTextSize(Vector2 targetSize, Vector2 originalSize, float duration)
    {
        float timer = 0f;
        while (timer < duration)
        {
            float progress = timer / duration;
            _scoreText.gameObject.transform.localScale = Vector2.Lerp(originalSize, targetSize, progress);
            timer += Time.deltaTime;
            yield return null;
        }

        _scoreText.gameObject.transform.localScale = targetSize;

        yield return new WaitForSeconds(_idleTimeBeforeShrink);

        timer = 0f;
        while (timer < duration)
        {
            float progress = timer / duration;
            _scoreText.gameObject.transform.localScale = Vector2.Lerp(targetSize, originalSize, progress);
            timer += Time.deltaTime;
            yield return null;
        }

        _scoreText.gameObject.transform.localScale = originalSize;
    }
}
