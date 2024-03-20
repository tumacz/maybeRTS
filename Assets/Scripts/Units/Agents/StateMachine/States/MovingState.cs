using UnityEngine;
using UnityEngine.AI;

public class MovingState : AgentState
{
    public MovingState(AgentStateMachine agentStateMachine, Vector3 destination) : base(agentStateMachine, destination)
    {
    }

    public override void Execute()
    {
        Debug.Log("Moving State");
        if (_agentStateMachine.NavMeshAgent.remainingDistance <= _agentStateMachine.NavMeshAgent.stoppingDistance)
        {
            _agentStateMachine.SetState(_agentStateMachine.RestingState);
        }
    }
}