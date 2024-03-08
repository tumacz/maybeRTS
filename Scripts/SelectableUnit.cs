using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SelectableUnit : MonoBehaviour
{
    private NavMeshAgent _agent;
    [SerializeField] private Renderer _renderer;
    //[SerializeField] private SpriteRenderer _selectionSprite;

    private void Awake()
    {
        SelectionManager.Instance.AvailableUnits.Add(this);
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _renderer.material.color = Color.white;
    }

    public void MoveTo(Vector3 position) //redo, respond dependent on ctx
    {
        _agent.SetDestination(position);
    }

    public void OnSelected()
    {
        //selectionsprite
        _renderer.material.color = Color.red;
    }
    public void OnDeselected()
    {
        //selectionsprite
        _renderer.material.color = Color.white;
    }

}
