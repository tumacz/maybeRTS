using System.Collections.Generic;
using UnityEngine;

public class UnitSelectionManager : MonoBehaviour
{
    public HashSet<SelectableUnit> SelectedUnits = new HashSet<SelectableUnit>();
    public HashSet<SelectableBuilding> SelectedBuildings = new HashSet<SelectableBuilding>();

    public List<ISelection> AvailableUnits = new List<ISelection>();

    public void Select(ISelection unit)
    {
        if (unit is SelectableUnit selectableUnit)
        {
            SelectedUnits.Add(selectableUnit);
        }
        else if (unit is SelectableBuilding selectableBuilding)
        {
            SelectedBuildings.Add(selectableBuilding);
        }

        unit.OnSelected();
    }

    public void Deselect(ISelection unit)
    {
        if (unit is SelectableUnit selectableUnit)
        {
            SelectedUnits.Remove(selectableUnit);
        }
        else if (unit is SelectableBuilding selectableBuilding)
        {
            SelectedBuildings.Remove(selectableBuilding);
        }

        unit.OnDeselected();
    }

    public void DeselectAll()
    {
        foreach (SelectableUnit unit in SelectedUnits)
        {
            unit.OnDeselected();
        }
        SelectedUnits.Clear();

        foreach (SelectableBuilding building in SelectedBuildings)
        {
            building.OnDeselected();
        }
        SelectedBuildings.Clear();
    }

    public bool IsSelected(ISelection unit)
    {
        if (unit is SelectableUnit selectableUnit)
        {
            return SelectedUnits.Contains(selectableUnit);
        }
        else if (unit is SelectableBuilding selectableBuilding)
        {
            return SelectedBuildings.Contains(selectableBuilding);
        }

        return false;
    }
}
