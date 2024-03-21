using System.Collections;
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

    public Vector3 _restPosition;
    private NavMeshAgent NavMeshAgent;

    public Dictionary<GameObject, Vector3> SpotedEnemies { get; private set; } = new Dictionary<GameObject, Vector3>();

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
        _restPosition = transform.position;
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

    public void PerformAttack()
    {
        StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        Debug.Log("Rozpoczêcie ataku");
        yield return new WaitForSeconds(3);
        Debug.Log("Zakoñczenie ataku");

        if (SpotedEnemies.Count == 0 && PreviousState != null)
        {   
            if(PreviousState == AttackingState)
            {
                SetState(RestingState);
            }
            SetState(PreviousState);
        }
    }
}