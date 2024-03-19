using UnityEngine;

public interface IRayProvider
{
    Ray CreateRayAtMousePosition();

    Camera GetCamera();
}