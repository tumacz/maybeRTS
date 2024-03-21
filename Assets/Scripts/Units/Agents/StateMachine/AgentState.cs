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
        //base check for ennemies
        if(_agentStateMachine.SpotedEnemies.Count>0 && _agentStateMachine._currentAgentState != _agentStateMachine.AttackingState)
        {
            _agentStateMachine.SetState(_agentStateMachine.AttackingState);
        }
    }

    public virtual void ChangeState()
    {
    }
}