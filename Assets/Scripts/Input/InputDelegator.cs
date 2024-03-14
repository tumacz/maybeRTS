using UnityEngine;
using UnityEngine.InputSystem;

public class InputDelegator : MonoBehaviour
{
    private PlayerInput _inputControls;
    private IResponse _response;

    private void Awake()
    {
        _inputControls = new PlayerInput();
        _response = GetComponent<IResponse>();
        _inputControls.MouseObjects.MouseLeft.started += ctx => _response?.OnStartSelection();
        _inputControls.MouseObjects.MouseLeft.canceled += ctx => _response?.OnEndSelection();
        _inputControls.MouseObjects.MouseRight.performed += ctx => _response?.OnRightMouseButton();
        _inputControls.MouseObjects.ExtendSelection.started += ctx => _response?.OnExtendSelectionStarted();
        _inputControls.MouseObjects.ExtendSelection.canceled += ctx => _response?.OnExtendSelectionCanceled();

        _inputControls.Enable();
    }

    private void OnDisable()
    {
        _inputControls.Disable();
    }
}