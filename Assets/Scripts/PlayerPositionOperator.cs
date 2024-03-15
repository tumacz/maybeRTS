using UnityEngine;

public class PlayerPositionOperator : MonoBehaviour, IPlayerResponse
{
    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _acceleration = .2f;
    [SerializeField] private float _deceleration = .2f;
    [SerializeField] private Transform _playerTransform;

    private float _moveUpValue = 0f;
    private float _moveDownValue = 0f;
    private float _moveLeftValue = 0f;
    private float _moveRightValue = 0f;

    private Vector3 _currentVelocity = Vector3.zero;

    public void MoveForward()
    {
        if (_moveDownValue > 0f)
        {
            _moveDownValue = 0f;
            _moveUpValue = 1f;
        }
        else
        {
            _moveUpValue = 1f;
        }
    }

    public void CancelForward()
    {
        _moveUpValue = 0f;
    }

    public void MoveBack()
    {
        if (_moveUpValue > 0f)
        {
            _moveUpValue = 0f;
            _moveDownValue = 1f;
        }
        else
        {
            _moveDownValue = 1f;
        }
    }

    public void CancelBack()
    {
        _moveDownValue = 0f;
    }

    public void MoveLeft()
    {
        if (_moveRightValue > 0f)
        {
            _moveRightValue = 0f;
            _moveLeftValue = 1f;
        }
        else
        {
            _moveLeftValue = 1f;
        }
    }

    public void CancelLeft()
    {
        _moveLeftValue = 0f;
    }

    public void MoveRight()
    {
        if (_moveLeftValue > 0f)
        {
            _moveLeftValue = 0f;
            _moveRightValue = 1f;
        }
        else
        {
            _moveRightValue = 1f;
        }
    }

    public void CancelRight()
    {
        _moveRightValue = 0f;
    }

    private void Update()
    {
        float moveX = _moveRightValue - _moveLeftValue;
        float moveZ = _moveUpValue - _moveDownValue;
        Vector3 targetVelocity = new Vector3(moveX, 0f, moveZ) * _playerSpeed;
        _currentVelocity = Vector3.Lerp(_currentVelocity, targetVelocity, Time.deltaTime * _acceleration);

        _playerTransform.position += _currentVelocity * Time.deltaTime;
        if (Mathf.Approximately(moveX, 0f) && Mathf.Approximately(moveZ, 0f))
        {
            _currentVelocity = Vector3.Lerp(_currentVelocity, Vector3.zero, Time.deltaTime * _deceleration);
        }
    }
}