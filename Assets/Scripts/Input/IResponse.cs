using UnityEngine;

public interface IResponse
{
    void OnStartSelection();
    void OnEndSelection();
    void OnRightMouseButton();
    void OnExtendSelectionStarted();
    void OnExtendSelectionCanceled();
}