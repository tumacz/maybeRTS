using UnityEngine;

public class RestingState : AgentState
{
    private Vector3 _previousPosition;
    private float _restingStateStoppingDistance = 0.5f;

    public RestingState(AgentStateMachine agentStateMachine, Vector3 destination) : base(agentStateMachine, destination)
    {
        _agentStateMachine._restingPosition = _agentStateMachine.transform.position;
        _agentStateMachine.NavMeshAgent.stoppingDistance = _restingStateStoppingDistance;
    }

    public override void Execute()
    {
        Debug.Log("Resting State");
        base.Execute();


        //_previousPosition = _agentStateMachine.transform.position;

        //if (_previousPosition != _agentStateMachine.transform.position)
        //{
        //    _agentStateMachine.SetState(_agentStateMachine.MovingState);
        //    _agentStateMachine.MoveTo(_previousPosition);
        //}
    }
}