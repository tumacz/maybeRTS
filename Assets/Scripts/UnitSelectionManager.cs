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

    public void CheckSelected()
    {
        if (SelectedUnits.Count > 0)
        {
            bool containsSelectableUnit = false;

            // Sprawdzamy, czy na liœcie jest przynajmniej jedna jednostka (Unit)
            foreach (ISelection selectedUnit in SelectedUnits)
            {
                if (selectedUnit.UnitType == Utiles.UnitType.Unit)
                {
                    containsSelectableUnit = true;
                    break;
                }
            }

            // Jeœli na liœcie jest przynajmniej jedna jednostka (Unit), dodajemy budynki (SelectableBuilding) do listy tymczasowej
            if (containsSelectableUnit)
            {
                List<ISelection> buildingsToDeselect = new List<ISelection>();

                foreach (ISelection selectableUnit in SelectedUnits)
                {
                    if (selectableUnit.UnitType == Utiles.UnitType.Bulding)
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