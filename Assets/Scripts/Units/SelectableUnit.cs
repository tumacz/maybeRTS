using UnityEngine;
using UnityEngine.AI;
using static Utiles;

[RequireComponent(typeof(NavMeshAgent))]
public class SelectableUnit : MonoBehaviour, ISelection
{
    private NavMeshAgent _agent;
    private UnitSelectionManager _selectionManager;
    private ISelectionResponse _selectionResponse;

    public Vector3 CurrentPosition => transform.position;

    public UnitType UnitType => UnitType.Unit;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
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

    private void MoveTo(Vector3 position)
    {
        if (_agent != null)
        {
            _agent.SetDestination(position);
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

    public void Respond(Vector3 position)
    {
        MoveTo(position);
    }
}
