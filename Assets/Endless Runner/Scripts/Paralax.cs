using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Paralax : MonoBehaviour
{
    [SerializeField] private RawImage[] _rawImages;
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _maxSpeed;

    private bool _isInitialized = false;

    private void OnEnable()
    {
        if (_isInitialized == false)
            Init();
    }

    private void OnDisable()
    {
        _isInitialized = false;
    }

    public void Init()
    {
        foreach (var rawImage in _rawImages)
        {
            StartCoroutine(Move(rawImage, Random.Range(_minSpeed, _maxSpeed)));
        }

        _isInitialized = true;
    }

    private IEnumerator Move(RawImage image, float speed)
    {
        while (_isInitialized == true)
        {

            image.uvRect = new Rect(image.uvRect.x, image.uvRect.y + (Time.deltaTime * speed), image.uvRect.width, image.uvRect.height);

            yield return null;
        }
    }
}
