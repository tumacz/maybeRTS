using UnityEngine;
using UnityEngine.AI;
using static UnitUtiles;

public class AgentStateMachine : MonoBehaviour
{
    public RestingState RestingState;
    public MovingState MovingState;
    public AttackingState AttackingState;

    protected AgentState _currentAgentState;
    protected Vector3 _currentDestination;
    protected Vector3 _currentPosition => transform.position;
    public NavMeshAgent NavMeshAgent;
    public Collider DetectionSphere;

    private void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        RestingState = new RestingState(this, _currentPosition);
        MovingState = new MovingState(this, _currentDestination);
        AttackingState = new AttackingState(this, _currentDestination);
    }

    private void Start()
    {
        SetState(RestingState);
    }

    private void Update()
    {
        _currentAgentState.Execute();
    }

    public void SetState(AgentState newState)
    {
        _currentAgentState = newState;
    }

    internal void ExecuteRequest(Vector3 position, LayerMask layer)
    {
        if (layer.value == LayerType.SurfaceLayer)
        {
            SetState(MovingState);
            MoveTo(position);
        }
        else if (layer.value == LayerType.UnitLayer)
        {
            Debug.Log(LayerMask.LayerToName(layer) + "  Position: " + position);
            
        }
    }

    private void MoveTo(Vector3 position)
    {
        if (NavMeshAgent != null)
        {
            NavMeshAgent.SetDestination(position);
        }
        else
        {
            Debug.LogWarning("NavMeshAgent is null.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerType.EnemyLayer)
        {
            Debug.Log("test");
            SetState(AttackingState);
            MoveTo(other.gameObject.transform.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("test exit");
    }
}