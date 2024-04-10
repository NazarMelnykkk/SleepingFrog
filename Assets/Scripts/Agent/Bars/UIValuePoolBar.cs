using UnityEngine;
using UnityEngine.UI;

public class UIValuePoolBar : ValueBarBase
{
    [SerializeField] protected Slider _currentBar;

    public void Show(ValuePool targetPool)
    {
        UpdateBar(targetPool.MaxValue.Integer_value, targetPool.CurrentValue);
    }

    public override void UpdateBar(int maxValue, int currentValue)
    {
        _currentBar.maxValue = maxValue;
        _currentBar.minValue = 0;
        _currentBar.value = currentValue;
    }

    public void Clear()
    {
        _currentBar.maxValue = 0;
        _currentBar.minValue = 0;
        _currentBar.value = 0;
    }
}
