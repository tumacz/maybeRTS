using UnityEngine;
using static Utiles;

public interface ISelection
{
    void OnDeselected();
    void OnSelected();
    void OnHoverEnter();
    void OnHoverExit();
    void Respond(Vector3 position);
    Vector3 CurrentPosition { get; }
    UnitType UnitType { get; }
}