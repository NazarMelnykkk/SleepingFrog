using UnityEngine;


public class AgentBarHandler : MonoBehaviour
{
    [SerializeField] private Agent _agent;
    [SerializeField] private ValueBarBase _valueBar;

    private void Awake()
    {
        if (_agent == null)
        {
            _agent = GetComponent<Agent>();
        }
    }

    private void OnEnable()
    {
        _agent.OnHealthChangedEvent += UpdateBar;

    }

    private void OnDisable()
    {
        _agent.OnHealthChangedEvent -= UpdateBar;
    }


    private void UpdateBar(ValuePool valuePool)
    {
        _valueBar.UpdateBar(valuePool.MaxValue.Integer_value, valuePool.CurrentValue);
    }
}
