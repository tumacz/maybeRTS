using UnityEngine;
using static UnitUtiles;
using static UnityEditor.Experimental.GraphView.GraphView;

public class SelectableBuilding : MonoBehaviour, ISelection
{
    private UnitSelectionManager _selectionManager;
    private ISelectionResponse _selectionResponse;

    public Vector3 CurrentPosition => transform.position;

    public UnitType UnitType => UnitType.Bulding;

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
        _selectionResponse.OnHoverEnterResponse();
    }

    public void OnHoverExit()
    {
        _selectionResponse.OnHoverExitResponse();
    }

    public void Respond(GameObject hit, Vector3 position)
    {
        Debug.Log(position);
        Debug.Log(hit.layer.ToString());
    }
}