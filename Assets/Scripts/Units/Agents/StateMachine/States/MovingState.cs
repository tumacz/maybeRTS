using UnityEngine;
using UnityEngine.AI;

public class MovingState : AgentState
{
    private float _moveStateStoppingDistance = 0.5f;

    public MovingState(AgentStateMachine agentStateMachine, NavMeshAgent navMeshAgent) : base(agentStateMachine, navMeshAgent)
    {
        navMeshAgent.stoppingDistance = _moveStateStoppingDistance;
    }

    public override void Execute()
    {
        base.Execute();

        if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
        {
            _agentStateMachine.SetState(_agentStateMachine.RestingState);
        }
    }
}