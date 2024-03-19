using UnityEngine;

public interface IPlayerResponse
{
    void MoveForward();
    void CancelForward();
    void MoveBack();
    void CancelBack();
    void MoveLeft();
    void CancelLeft();
    void MoveRight();
    void CancelRight();
}
