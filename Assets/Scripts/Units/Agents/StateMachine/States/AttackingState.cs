using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackingState : AgentState
{
    private float _attackDistance = 4f;

    public AttackingState(AgentStateMachine agentStateMachine, Vector3 destination) : base(agentStateMachine, destination)
    {
        _agentStateMachine.NavMeshAgent.stoppingDistance = _attackDistance;
    }

    public override void Execute()
    {
        Debug.Log("Attacking State");

        if (_agentStateMachine.SpotedEnemies.Count > 0)
        {
            // Pocz¹tkowo ustaw najbli¿sz¹ odleg³oœæ na du¿¹ liczbê
            float closestDistance = Mathf.Infinity;
            // Ustaw pocz¹tkowo cel na null
            GameObject closestEnemy = null;

            // Iteruj przez wszystkich wrogów
            foreach (var enemy in _agentStateMachine.SpotedEnemies)
            {
                // Oblicz odleg³oœæ miêdzy agentem a wrogiem
                float distanceToEnemy = Vector3.Distance(_agentStateMachine.transform.position, enemy.Value);
                // Jeœli odleg³oœæ jest mniejsza od aktualnej najbli¿szej odleg³oœci
                if (distanceToEnemy < closestDistance)
                {
                    // Ustaw aktualnego wroga jako najbli¿szego wroga
                    closestDistance = distanceToEnemy;
                    closestEnemy = enemy.Key;
                }
            }

            // Jeœli najbli¿szy wróg zosta³ znaleziony
            if (closestEnemy != null)
            {
                // Ustaw pozycjê najbli¿szego wroga jako cel ataku
                _agentStateMachine.MoveTo(closestEnemy.transform.position);
                float distanceToEnemy = Vector3.Distance(_agentStateMachine.transform.position, closestEnemy.transform.position);
                if (distanceToEnemy <= _attackDistance)
                {
                    _agentStateMachine.PerformAttack();
                }
            }
        }
        else
        {
            // Jeœli nie ma wrogów, przejdŸ do stanu odpoczynku
            _agentStateMachine.SetState(_agentStateMachine._previousState);
        }
    }
}