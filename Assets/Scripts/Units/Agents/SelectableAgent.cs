using UnityEngine;
using UnityEngine.AI;
using static UnitUtiles;

[RequireComponent (typeof(AgentType))]
public class SelectableAgent : MonoBehaviour, ISelection
{

    private AgentType _agentType;
    private UnitSelectionManager _selectionManager;
    private ISelectionResponse _selectionResponse;

    public Vector3 CurrentPosition => transform.position;

    public UnitType UnitType => UnitType.Unit;

    private void Awake()
    {
        _agentType = GetComponent<AgentType>();
        _selectionResponse = GetComponent<ISelectionResponse>();
        _selectionManager = FindObjectOfType<UnitSelectionManager>();
    }

    private void Start()
    {
        if(_selectionManager != null)
        {
            _selectionManager.AvailableUnits.Add(this);
        }
    }

    public void OnSelected()
    {
        if( _selectionResponse != null )
        {
            _selectionResponse.OnSelectedResponse();
        }
    }

    public void OnDeselected()
    {
        if(_selectionResponse != null )
        {
            _selectionResponse.OnDeselectedResponse();
        }
    }

    public void OnHoverEnter()
    {
        if (_selectionResponse != null)
        {
            _selectionResponse.OnHoverEnterResponse();
        }
    }

    public void OnHoverExit()
    {
        if (_selectionResponse != null)
        {
            _selectionResponse.OnHoverExitResponse();
        }
    }

    public void Respond(Vector3 position, LayerMask layer)
    {
        _agentType.ExecuteRespond(position, layer);
    }
}