using UnityEngine;

public interface IControllerLeftResponse
{
    void OnStartSelection();
    void OnEndSelection();
    void OnExtendSelectionStarted();
    void OnExtendSelectionCanceled();
}