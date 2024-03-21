using UnityEngine;
using static UnitUtiles;

public interface ISelection
{
    void OnDeselected();
    void OnSelected();
    void OnHoverEnter();
    void OnHoverExit();
    void Respond(GameObject hit, Vector3 position);
    Vector3 CurrentPosition { get; }
    UnitType UnitType { get; }
}