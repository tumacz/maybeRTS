using UnityEngine;
using UnityEngine.AI;
using static UnitUtiles;

[RequireComponent(typeof(NavMeshAgent))]
public class AgentType : MonoBehaviour
{
    private NavMeshAgent _agentNav;

    private void Awake()
    {
        _agentNav = GetComponent<NavMeshAgent>();
    }

    internal void ExecuteRespond(Vector3 position, LayerMask layer)
    {
        if (layer.value == LayerType.SurfaceLayer)
        {
            MoveTo(position);
        }
        else if (layer.value == LayerType.UnitLayer)
        {
            Debug.Log(LayerMask.LayerToName(layer) + "  Position: " + position);
        }
    }

    private void MoveTo(Vector3 position)
    {
        if (_agentNav != null)
        {
            _agentNav.SetDestination(position);
        }
    }
}