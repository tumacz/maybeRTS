using UnityEngine;
using UnityEngine.InputSystem;

public class InputDelegator : MonoBehaviour
{
    private PlayerInput _inputControls;
    private IControllerResponse _response;
    private IPlayerResponse _playerResponse;

    private void Awake()
    {
        _inputControls = new PlayerInput();
        _response = GetComponent<IControllerResponse>();
        _playerResponse = GetComponent<IPlayerResponse>();

        _inputControls.Mouse.MouseLeft.started += ctx => _response?.OnStartSelection();
        _inputControls.Mouse.MouseLeft.canceled += ctx => _response?.OnEndSelection();
        _inputControls.Mouse.MouseRight.performed += ctx => _response?.OnRightMouseButton();

        _inputControls.Keyboard.ExtendSelection.started += ctx => _response?.OnExtendSelectionStarted();
        _inputControls.Keyboard.ExtendSelection.canceled += ctx => _response?.OnExtendSelectionCanceled();

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