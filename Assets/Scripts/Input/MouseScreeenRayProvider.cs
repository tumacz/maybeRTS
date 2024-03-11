using UnityEngine;
using UnityEngine.InputSystem;

public class MouseScreeenRayProvider : MonoBehaviour, IRayProvider
{
    private Camera _camera; //redo, cant be main
    private void Awake()
    {
        _camera = Camera.main;
    }
    public Ray CreateRayAtMousePosition()
    {
        return _camera.ScreenPointToRay(Mouse.current.position.ReadValue());
    }
}