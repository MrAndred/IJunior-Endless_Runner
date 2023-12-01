using System.Collections;
using UnityEngine;

public class PlayerMover
{
    private Player _player;
    private float _duration;
    private float _step;

    private float _leftBound;
    private float _rightBound;

    private Vector3 _targetPosition;
    private Coroutine _moving;

    public PlayerMover(Player player, float duration, float leftBound, float rightBound, float step)
    {
        _targetPosition = player.gameObject.transform.position;
        _player = player;
        _duration = duration;
        _leftBound = leftBound;
        _rightBound = rightBound;
        _step = step;
    }

    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (_leftBound < _player.gameObject.transform.position.x - _step)
                Move(-_step);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (_rightBound > _player.gameObject.transform.position.x + _step)
                Move(_step);
        }
    }

    private void Move(float step)
    {
        if (_moving != null)
        {
            _player.StopCoroutine(_moving);
        }

        float targetPositionX = _targetPosition.x + step;
        _moving = _player.StartCoroutine(MoveToX(targetPositionX));

        _targetPosition = new Vector3(targetPositionX, _targetPosition.y, _targetPosition.z);
    }

    private IEnumerator MoveToX(float targetPositionX)
    {
        float time = 0;

        while (_player.gameObject.transform.position.x != targetPositionX)
        {
            time += Time.deltaTime;

            _player.gameObject.transform.position = Vector3.Lerp(_player.gameObject.transform.position,
                               new Vector3(targetPositionX, _player.gameObject.transform.position.y, _player.gameObject.transform.position.z),
                                              time / _duration);
            yield return null;
        }
    }
}
