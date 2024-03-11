using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SelectableUnit : MonoBehaviour, ISelection
{
    private NavMeshAgent _agent;
    private UnitSelectionManager _selectionManager;
    private ISelectionResponse _selectionResponse;

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

    public void MoveTo(Vector3 position)
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
}
