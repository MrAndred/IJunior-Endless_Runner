using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameRoot : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private List<EnemySpawner> _enemySpawners;
    [SerializeField] private AidKitSpawner _aidKitSpawner;

    [SerializeField] private Paralax _paralax;
    [SerializeField] private CanvasGroup _loseCanvas;
    [SerializeField] private Button _restartButton;

    [SerializeField] private float _leftBound;
    [SerializeField] private float _rightBound;

    private float _maxAlfa = 1f;
    private float _minAlfa = 0f;

    private void OnEnable()
    {
        _player.OnDeath += ShowLoseScreen;
        _restartButton.onClick.AddListener(OnRestartButtonClick);
    }

    private void OnDisable()
    {
        _player.OnDeath -= ShowLoseScreen;
        _restartButton.onClick.RemoveListener(OnRestartButtonClick);
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _loseCanvas.alpha = _minAlfa;
        _paralax.Init();
        _player.Init(_leftBound, _rightBound);
        _aidKitSpawner.Init(_player.transform.position.y, _leftBound, _rightBound);

        foreach (EnemySpawner enemySpawner in _enemySpawners)
        {
            enemySpawner.Init();
        }
    }

    private void ShowLoseScreen()
    {
        StartCoroutine(Lose());
    }

    private void OnRestartButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator Lose()
    {
        while (_loseCanvas.alpha < _maxAlfa)
        {
            _loseCanvas.alpha += Time.deltaTime;
            yield return null;
        }
    }
}
