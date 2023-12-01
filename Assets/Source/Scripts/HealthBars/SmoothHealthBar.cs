using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SmoothHealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private float _smoothSpeed = 0.5f;

    private Health _health;
    private float _maxHealth;
    private bool _isInitialized;

    private Coroutine _changeSliderValueCoroutine;

    private void OnEnable()
    {
        if (_isInitialized == true)
        {
            _health.OnHealthChanged += HandleHealthChanged;
        }
    }

    private void OnDisable()
    {
        _health.OnHealthChanged -= HandleHealthChanged;
    }

    public void Init(Health health)
    {
        _health = health;
        _maxHealth = health.Value;

        _slider.maxValue = _maxHealth;
        _slider.value = _maxHealth;
        _isInitialized = true;

        _health.OnHealthChanged += HandleHealthChanged;
    }

    private void HandleHealthChanged()
    {
        float value = _health.Value;

        if (_changeSliderValueCoroutine != null)
        {
            StopCoroutine(_changeSliderValueCoroutine);
        }

        _changeSliderValueCoroutine = StartCoroutine(ChangeSliderValue(value));
    }

    private IEnumerator ChangeSliderValue(float value)
    {
        while (_slider.value != value)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, value, _smoothSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
