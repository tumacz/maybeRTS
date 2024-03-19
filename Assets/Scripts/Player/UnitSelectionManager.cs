using System.Collections.Generic;
using UnityEngine;
using static UnitUtiles;

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

    public void CheckSelected()
    {
        if (SelectedUnits.Count > 0)
        {
            bool containsSelectableUnit = false;

            foreach (ISelection selectedUnit in SelectedUnits)
            {
                if (selectedUnit.UnitType == UnitType.Unit)
                {
                    containsSelectableUnit = true;
                    break;
                }
            }

            if (containsSelectableUnit)
            {
                List<ISelection> buildingsToDeselect = new List<ISelection>();

                foreach (ISelection selectableUnit in SelectedUnits)
                {
                    if (selectableUnit.UnitType == UnitType.Bulding)
                    {
                        buildingsToDeselect.Add(selectableUnit);
                    }
                }

                // Odznaczamy budynki (SelectableBuilding)
                foreach (ISelection building in buildingsToDeselect)
                {
                    building.OnDeselected();
                    Deselect(building);
                }
            }
        }
    }

}