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
            // Pocz�tkowo ustaw najbli�sz� odleg�o�� na du�� liczb�
            float closestDistance = Mathf.Infinity;
            // Ustaw pocz�tkowo cel na null
            GameObject closestEnemy = null;

            // Iteruj przez wszystkich wrog�w
            foreach (var enemy in _agentStateMachine.SpotedEnemies)
            {
                // Oblicz odleg�o�� mi�dzy agentem a wrogiem
                float distanceToEnemy = Vector3.Distance(_agentStateMachine.transform.position, enemy.Value);
                // Je�li odleg�o�� jest mniejsza od aktualnej najbli�szej odleg�o�ci
                if (distanceToEnemy < closestDistance)
                {
                    // Ustaw aktualnego wroga jako najbli�szego wroga
                    closestDistance = distanceToEnemy;
                    closestEnemy = enemy.Key;
                }
            }

            // Je�li najbli�szy wr�g zosta� znaleziony
            if (closestEnemy != null)
            {
                // Ustaw pozycj� najbli�szego wroga jako cel ataku
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
            // Je�li nie ma wrog�w, przejd� do stanu odpoczynku
            _agentStateMachine.SetState(_agentStateMachine._previousState);
        }
    }
}