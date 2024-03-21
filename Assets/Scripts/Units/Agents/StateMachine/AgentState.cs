using UnityEngine;
using UnityEngine.AI;
using static UnitUtiles;

public abstract class AgentState
{
    protected AgentStateMachine _agentStateMachine { get; private set; }
    protected NavMeshAgent _navMeshAgent { get; private set; }
    protected Vector3 _destination {  get; private set; }


    protected AgentState(AgentStateMachine agentStateMachine, NavMeshAgent navMeshAgent)
    {
        _agentStateMachine = agentStateMachine;
        _navMeshAgent = navMeshAgent;
    }

    public virtual void Execute()
    {
        if (_agentStateMachine.SpotedEnemies.Count > 0 && _agentStateMachine.CurrentAgentState != _agentStateMachine.AttackingState)
        {
            _agentStateMachine.SetState(_agentStateMachine.AttackingState);
        }
    }

    public virtual void ChangeState(Vector3 position, AgentState state)
    {
        MoveTo(position);
        _destination = position;
        _agentStateMachine._restPosition = _destination;
        _agentStateMachine.SetState(state);
    }

    public void ExecuteRequest(GameObject hit, Vector3 position)
    {
        if (hit.layer == LayerType.SurfaceLayer)
        {
            _destination = position;
            ChangeState(position, _agentStateMachine.MovingState);
        }
        else if (hit.layer == LayerType.UnitLayer)
        {
            Debug.Log(LayerMask.LayerToName(hit.layer) + "  Position: " + hit.transform.position);
        }
        else if (hit.layer == LayerType.EnemyLayer)
        {
            _destination = hit.transform.position;
            ChangeState(position, _agentStateMachine.AttackingState);
        }
    }

    public void MoveTo(Vector3 position)
    {
        if (_navMeshAgent != null)
        {
            _navMeshAgent.SetDestination(position);
        }
        else
        {
            Debug.LogWarning("NavMeshAgent is null.");
        }
    }
}