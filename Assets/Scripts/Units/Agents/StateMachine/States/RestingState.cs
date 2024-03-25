using UnityEngine;
using UnityEngine.AI;

public class RestingState : AgentState
{
    private float _restingStateStoppingDistance = 0f;

    public RestingState(AgentStateMachine agentStateMachine, NavMeshAgent navMeshAgent) : base(agentStateMachine, navMeshAgent)
    {
        navMeshAgent.stoppingDistance = _restingStateStoppingDistance;
    }

    public override void Execute()
    {
        base.Execute();
    }
}