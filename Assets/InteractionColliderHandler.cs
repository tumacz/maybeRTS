using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.Diagnostics;

public class InteractionColliderHandler : MonoBehaviour
{
    [SerializeField] AgentStateMachine _agentStateMachine;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == UnitUtiles.LayerType.EnemyLayer)
        {
            _agentStateMachine.SpotedEnemies.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_agentStateMachine.SpotedEnemies.Contains(other.gameObject))
        {
            _agentStateMachine.SpotedEnemies.Remove(other.gameObject);
        }
    }
}