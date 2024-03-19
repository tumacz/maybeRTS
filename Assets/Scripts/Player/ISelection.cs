using UnityEngine;
using static UnitUtiles;

public interface ISelection
{
    void OnDeselected();
    void OnSelected();
    void OnHoverEnter();
    void OnHoverExit();
    void Respond(Vector3 position, LayerMask layer);
    Vector3 CurrentPosition { get; }
    UnitType UnitType { get; }
}