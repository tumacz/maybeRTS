using UnityEngine;
using UnityEngine.AI;
using static UnitUtiles;

[RequireComponent (typeof(AgentStateMachine))]
public class SelectableAgent : MonoBehaviour, ISelection
{
    private AgentStateMachine _agentStateMachine;
    private UnitSelectionManager _selectionManager;
    private ISelectionResponse _selectionResponse;

    public Vector3 CurrentPosition => transform.position;

    public UnitType UnitType => UnitType.Unit;

    private void Awake()
    {
        _agentStateMachine = GetComponent<AgentStateMachine>();
        _selectionResponse = GetComponent<ISelectionResponse>();
        _selectionManager = FindObjectOfType<UnitSelectionManager>();//redo zenject
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
        _agentStateMachine.ExecuteRequest(position, layer);
    }
}