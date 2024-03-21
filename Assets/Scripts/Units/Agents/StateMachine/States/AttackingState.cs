using UnityEngine;
using UnityEngine.AI;

public class AttackingState : AgentState
{
    private float _attackDistance = 5f;

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

            // Iterate through all enemies
            foreach (var enemy in _agentStateMachine.SpotedEnemies)
            {
                float distanceToEnemy = Vector3.Distance(_agentStateMachine.transform.position, enemy.Value);
                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = enemy.Key;
                }
            }

            if (closestEnemy != null)
            {
                // Set the nearest enemy's position as the attack target
                MoveTo(closestEnemy.transform.position);
                float distanceToEnemy = Vector3.Distance(_agentStateMachine.transform.position, closestEnemy.transform.position);
                if (distanceToEnemy <= _attackDistance)
                {
                    _agentStateMachine.PerformAttack();
                }
            }
        }
        else
        {
            _agentStateMachine.SetState(_agentStateMachine.PreviousState);
        }
    }
}