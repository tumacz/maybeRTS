using UnityEngine;
using UnityEngine.InputSystem;

public class InputDelegator : MonoBehaviour
{
    private PlayerInput _inputControls;
    private IControllerLeftResponse _leftResponse;
    private IPlayerResponse _playerResponse;
    private IControllerRightResponse _rightResponse;

    private void Awake()
    {
        _inputControls = new PlayerInput();
        _leftResponse = GetComponent<IControllerLeftResponse>();
        _rightResponse = GetComponent<IControllerRightResponse>();
        _playerResponse = GetComponent<IPlayerResponse>();

        _inputControls.Mouse.MouseLeft.started += ctx => _leftResponse?.OnStartSelection();
        _inputControls.Mouse.MouseLeft.canceled += ctx => _leftResponse?.OnEndSelection();
        _inputControls.Mouse.MouseRight.performed += ctx => _rightResponse?.OnRightMouseButton();

        _inputControls.Keyboard.ExtendSelection.started += ctx => _leftResponse?.OnExtendSelectionStarted();
        _inputControls.Keyboard.ExtendSelection.canceled += ctx => _leftResponse?.OnExtendSelectionCanceled();

        _inputControls.Keyboard.MoveForward.started += ctx => _playerResponse?.MoveForward();
        _inputControls.Keyboard.MoveForward.canceled += ctx => _playerResponse?.CancelForward();
        _inputControls.Keyboard.MoveBack.started += ctx => _playerResponse?.MoveBack();
        _inputControls.Keyboard.MoveBack.canceled += ctx => _playerResponse?.CancelBack();
        _inputControls.Keyboard.MoveRight.started += ctx => _playerResponse?.MoveRight();
        _inputControls.Keyboard.MoveRight.canceled += ctx => _playerResponse?.CancelRight();
        _inputControls.Keyboard.MoveLeft.started += ctx => _playerResponse?.MoveLeft();
        _inputControls.Keyboard.MoveLeft.canceled += ctx => _playerResponse?.CancelLeft();

        _inputControls.Enable();
    }

    private void OnDisable()
    {
        _inputControls.Disable();
    }
}