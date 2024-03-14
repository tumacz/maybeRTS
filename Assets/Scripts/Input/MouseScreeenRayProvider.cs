using UnityEngine;
using UnityEngine.InputSystem;

public class MouseScreeenRayProvider : MonoBehaviour, IRayProvider
{
    [SerializeField] private Camera _camera;

    public Ray CreateRayAtMousePosition()
    {
        return _camera.ScreenPointToRay(Mouse.current.position.ReadValue());
    }

    public Camera GetCamera()
    {
        return _camera;
    }
}