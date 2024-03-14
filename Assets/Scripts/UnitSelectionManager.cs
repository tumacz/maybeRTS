using System.Collections.Generic;
using UnityEngine;

public class UnitSelectionManager : MonoBehaviour
{
    public HashSet<ISelection> SelectedUnits = new HashSet<ISelection>();

    public List<ISelection> AvailableUnits = new List<ISelection>();

    public void Select(ISelection unit)
    {
        SelectedUnits.Add(unit);
    }

    public void Deselect(ISelection unit)
    {
        SelectedUnits.Remove(unit);
    }

    public void DeselectAll()
    {
        foreach (ISelection unit in SelectedUnits)
        {
            unit.OnDeselected();
        }
        SelectedUnits.Clear();
    }

    public void DehoverAll()
    {
        foreach (ISelection unit in SelectedUnits)
        {
            unit.OnHoverExit();
            unit.OnSelected();
        }
    }

    public bool IsSelected(ISelection unit)
    {
        return SelectedUnits.Contains(unit);
    }
}