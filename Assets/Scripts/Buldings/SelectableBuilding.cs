using UnityEngine;

public class SelectableBuilding : MonoBehaviour, ISelection
{
    private UnitSelectionManager _selectionManager;
    private ISelectionResponse _selectionResponse;

    public Vector3 CurrentPosition => transform.position;

    private void Awake()
    {
        _selectionManager = FindObjectOfType<UnitSelectionManager>();
        _selectionResponse = GetComponent<ISelectionResponse>();
    }

    private void Start()
    {
        if (_selectionManager != null)
        {
            _selectionManager.AvailableUnits.Add(this);
        }
    }

    public void OnDeselected()
    {
        _selectionResponse.OnDeselectedResponse();
    }

    public void OnSelected()
    {
        _selectionResponse.OnSelectedResponse();
    }

    public void OnHoverEnter()
    {
        throw new System.NotImplementedException();
    }

    public void OnHoverExit()
    {
        throw new System.NotImplementedException();
    }
}