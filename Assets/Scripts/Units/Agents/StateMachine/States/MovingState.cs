using UnityEngine;
using UnityEngine.AI;

public class MovingState : AgentState
{
    private float _moveStateStoppingDistance = 0.5f;

    public MovingState(AgentStateMachine agentStateMachine, Vector3 destination) : base(agentStateMachine, destination)
    {
        _agentStateMachine.NavMeshAgent.stoppingDistance = _moveStateStoppingDistance;
    }

    public override void Execute()
    {
        Debug.Log("Moving State");
        base.Execute();

        _agentStateMachine.MoveTo(Destination);

        if (_agentStateMachine.NavMeshAgent.remainingDistance <= _agentStateMachine.NavMeshAgent.stoppingDistance)
        {
            _agentStateMachine.SetState(_agentStateMachine.RestingState);
        }
    }
}