using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnitUtiles;

public class AgentStateMachine : MonoBehaviour
{
    public RestingState RestingState;
    public MovingState MovingState;
    public AttackingState AttackingState;

    public AgentState _currentAgentState;
    public AgentState _previousState;
    private Vector3 _currentDestination;
    public Vector3 _restingPosition;
    private Vector3 _currentPosition => transform.position;
    public NavMeshAgent NavMeshAgent;
    private Collider DetectionSphere;


    public Dictionary<GameObject, Vector3> SpotedEnemies = new Dictionary<GameObject, Vector3>();

    private void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        RestingState = new RestingState(this, _currentPosition);
        MovingState = new MovingState(this, _currentDestination);
        AttackingState = new AttackingState(this, _currentDestination);
    }

    private void Start()
    {
        SetState(RestingState);
        _restingPosition = transform.position;
        MovingState.Destination = _restingPosition;
    }

    private void Update()
    {
        _currentAgentState.Execute();
    }

    public void SetState(AgentState newState)
    {
        _previousState = _currentAgentState;
        _currentAgentState = newState;
    }

    internal void ExecuteRequest(GameObject hit, Vector3 position)
    {
        if (hit.layer == LayerType.SurfaceLayer)
        {
            MovingState.Destination = position;
            MoveTo(position);
            SetState(MovingState);
        }
        else if (hit.layer == LayerType.UnitLayer)
        {
            Debug.Log(LayerMask.LayerToName(hit.layer) + "  Position: " + hit.transform.position);
        }
        else if (hit.layer == LayerType.EnemyLayer)
        {
            AttackingState.Destination = hit.transform.position;
            MoveTo(position);
            SetState(AttackingState);
        }
    }

    public void MoveTo(Vector3 position)
    {
        if (NavMeshAgent != null)
        {
            NavMeshAgent.SetDestination(position);
        }
        else
        {
            Debug.LogWarning("NavMeshAgent is null.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerType.EnemyLayer)
        {
            Debug.Log("Enemy entered");
            SpotedEnemies.Add(other.gameObject, other.gameObject.transform.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerType.EnemyLayer)
        {
            Debug.Log("Enemy exited");
            SpotedEnemies.Remove(other.gameObject);
            MoveTo(_restingPosition);
            SetState(MovingState);
        }
    }

    public void PerformAttack()
    {
        StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        Debug.Log("Rozpoczêcie ataku");
        yield return new WaitForSeconds(1);
        Debug.Log("Zakoñczenie ataku");

        if (SpotedEnemies.Count == 0 && _previousState != null)
        {   
            if(_previousState == AttackingState)
            {
                SetState(RestingState);
            }
            SetState(_previousState);
        }
    }
}