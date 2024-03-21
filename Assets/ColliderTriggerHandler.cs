using UnityEngine;
using static UnitUtiles;

public class ColliderTriggerHandler : MonoBehaviour
{
    [SerializeField] private AgentStateMachine _agentStateMachine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerType.EnemyLayer)
        {
            _agentStateMachine.SpotedEnemies.Add(other.gameObject, other.gameObject.transform.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerType.EnemyLayer)
        {
            _agentStateMachine.SpotedEnemies.Remove(other.gameObject);

            if (_agentStateMachine.SpotedEnemies.Count == 0)
            {
                _agentStateMachine.CurrentAgentState.MoveTo(_agentStateMachine._restPosition);
                _agentStateMachine.SetState(_agentStateMachine.MovingState);
            }
        }
    }
}