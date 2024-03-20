using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackingState : AgentState
{
    public AttackingState(AgentStateMachine agentStateMachine, Vector3 destionation) : base(agentStateMachine, destionation)
    {
    }

    public override void Execute()
    {
        Debug.Log("Attacking State");
        if (_agentStateMachine.NavMeshAgent.remainingDistance <= _agentStateMachine.NavMeshAgent.stoppingDistance)
        {
            _agentStateMachine.SetState(_agentStateMachine.RestingState);
        }
    }

    public override void ChangeState()
    {
        _agentStateMachine.SetState(_agentStateMachine.MovingState);
    }
}