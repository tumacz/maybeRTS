using UnityEngine;
using UnityEngine.AI;

public abstract class AgentState
{
    protected AgentStateMachine _agentStateMachine;
    public Vector3 Destination;

    protected AgentState(AgentStateMachine agentStateMachine, Vector3 destination)
    {
        _agentStateMachine = agentStateMachine;
        Destination = destination;
    }

    public virtual void Execute()
    {

    }

    public virtual void ChangeState()
    {
    }
}