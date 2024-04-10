using UnityEngine;

public class CharacterUI : MonoBehaviour
{
    [SerializeField] private Agent _agent;
    [SerializeField] private UIValuePoolBar _healthBar;

    private void OnEnable()
    {
        _agent.OnHealthChangedEvent += UpdateHealthBar;
    }

    private void OnDisable()
    {
        _agent.OnHealthChangedEvent -= UpdateHealthBar;
    }

    private void UpdateHealthBar(ValuePool valuePool)
    {
        _healthBar.UpdateBar(valuePool.MaxValue.Integer_value, valuePool.CurrentValue);

    }

}
