using UnityEngine;

public class RestingState : AgentState
{
    public RestingState(AgentStateMachine agentStateMachine, Vector3 destination) : base(agentStateMachine, destination)
    {
    }
    public override void Execute()
    {
    }

    public override void ChangeState()
    {
    }
}