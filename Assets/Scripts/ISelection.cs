using UnityEngine;

public interface ISelection
{
    void OnDeselected();
    void OnSelected();
    void OnHoverEnter();
    void OnHoverExit();

    Vector3 CurrentPosition { get; }
}