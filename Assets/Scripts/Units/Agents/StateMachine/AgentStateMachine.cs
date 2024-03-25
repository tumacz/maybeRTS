using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AgentStateMachine : MonoBehaviour
{
    #region States
    public RestingState RestingState { get; private set; }
    public MovingState MovingState { get; private set; }
    public AttackingState AttackingState { get; private set; }

    public AgentState CurrentAgentState { get; private set; }
    public AgentState PreviousState { get; private set; }
    #endregion

    private NavMeshAgent NavMeshAgent;
    public Vector3 CurrentPostion { get; private set; }
    public Vector3 RestingPosition;

    public HashSet<GameObject> SpotedEnemies = new HashSet<GameObject>();

    private void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        RestingState = new RestingState(this, NavMeshAgent);
        MovingState = new MovingState(this, NavMeshAgent);
        AttackingState = new AttackingState(this, NavMeshAgent);
    }

    private void Start()
    {
        SetState(RestingState);
        CurrentPostion= transform.position;
        RestingPosition = CurrentPostion;
    }

    private void Update()
    {
        CurrentAgentState.Execute();
    }

    public void SetState(AgentState newState)
    {
        PreviousState = CurrentAgentState;
        CurrentAgentState = newState;
    }
}