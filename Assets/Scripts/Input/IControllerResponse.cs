using UnityEngine;

public interface IControllerResponse
{
    void OnStartSelection();
    void OnEndSelection();
    void OnRightMouseButton();
    void OnExtendSelectionStarted();
    void OnExtendSelectionCanceled();
}