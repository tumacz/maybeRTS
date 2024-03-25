using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AttackingState : AgentState
{
    private float _attackDistance = 4f;

    public AttackingState(AgentStateMachine agentStateMachine, NavMeshAgent navMeshAgent) : base(agentStateMachine, navMeshAgent)
    {
        navMeshAgent.stoppingDistance = _attackDistance;
    }

    public override void Execute()
    {
        if (_agentStateMachine.SpotedEnemies.Count > 0)
        {
            float closestDistance = Mathf.Infinity;
            GameObject closestEnemy = null;

            foreach (var enemy in _agentStateMachine.SpotedEnemies) //!!!
            {
                float distanceToEnemy = Vector3.Distance(_agentStateMachine.transform.position, enemy.transform.position);

                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = enemy;
                }
            }

            if (closestEnemy != null)
            {
                MoveTo(closestEnemy.transform.position);
                float distanceToEnemy = Vector3.Distance(_agentStateMachine.transform.position, closestEnemy.transform.position);
                if (distanceToEnemy <= _attackDistance)
                {
                    PerformAttack();
                }
            }
        }
        else
        {
            MoveTo(_agentStateMachine.RestingPosition);
            _agentStateMachine.SetState(_agentStateMachine.PreviousState);
        }
    }

    public void PerformAttack()
    {
        _agentStateMachine.StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        Debug.Log("Rozpocz?cie ataku");
        yield return new WaitForSeconds(3);
        Debug.Log("Zako?czenie ataku");

        if (_agentStateMachine.SpotedEnemies.Count == 0)
        {
            if (_agentStateMachine.PreviousState == null || _agentStateMachine.PreviousState != _agentStateMachine.AttackingState)
            {
                _agentStateMachine.SetState(_agentStateMachine.RestingState);
            }
            else if (_agentStateMachine.PreviousState != null)
            {
                _agentStateMachine.SetState(_agentStateMachine.PreviousState);
            }
        }
    }

}