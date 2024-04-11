using System;
using System.Collections.Generic;
using UnityEngine;

public enum Statistic
{
    Health,
    Damage,
    MoveSpeed,

}

[Serializable]
public class StatsValue
{
    public Statistic StatisticType;
    public bool IsfloatType;

    public int Integer_value;

    public float Float_value;

    public StatsValue(Statistic statisticType, int value = 0)
    {
        StatisticType = statisticType;
        Integer_value = value;
        IsfloatType = false;
    }

    public StatsValue(Statistic statisticType, float float_value = 0f)
    {
        StatisticType = statisticType;
        Float_value = float_value;
        IsfloatType = true;
    }
}

[Serializable]
public class StatsGroup
{
    public List<StatsValue> Stats;

    public StatsGroup()
    {
        Stats = new List<StatsValue>();
        Init();
    }

    public void Init()
    {
        Stats.Add(new StatsValue(Statistic.Health, 0));
        Stats.Add(new StatsValue(Statistic.Damage, 0));
        Stats.Add(new StatsValue(Statistic.MoveSpeed, 0f));
    }

    public StatsValue Get(Statistic statisticToGet)
    {
        return Stats[(int)statisticToGet];
    }

    public void Sum(StatsValue toAdd)
    {
        StatsValue statsValue = Stats[(int)toAdd.StatisticType];

        if (toAdd.IsfloatType == true)
        {
            statsValue.Float_value += toAdd.Float_value;
        }

        if (toAdd.IsfloatType == false)
        {
            statsValue.Integer_value += toAdd.Integer_value;
        }
    }

    public void Subtract(StatsValue toSubtract)
    {
        StatsValue statsValue = Stats[(int)toSubtract.StatisticType];

        if (toSubtract.IsfloatType == true)
        {
            statsValue.Float_value -= toSubtract.Float_value;
        }
        else
        {
            statsValue.Integer_value -= toSubtract.Integer_value;
        }
    }
}

[Serializable]
public class ValuePool
{
    public StatsValue MaxValue;
    public int CurrentValue;

    public ValuePool(StatsValue maxValue)
    {
        MaxValue = maxValue;
        CurrentValue = maxValue.Integer_value;
    }

    public void FullRestore()
    {
        CurrentValue = MaxValue.Integer_value;

    }
}

public class Agent : PoolableObject, IDamageable
{

    public ValuePool HealthPool;

    public bool isDead = false;

    public event Action<ValuePool> OnHealthChangedEvent;
    public event Action OnDieEvent;

    [Header("Configuration")]
    [SerializeField] private StatsGroup _stats;
    private AgentConfigBase _agentConfigBase;

    [Header("Components")]
    [SerializeField] private AgentAnimationController _agentAnimationController;

    public void Awake()
    {
        HealthPool = new ValuePool(_stats.Get(Statistic.Health));
    }

    public void Init(AgentConfigBase newConfig)
    {
        _agentConfigBase = newConfig;

        AddStats(_agentConfigBase.StatsValues);

        GetComponent<AgentAnimationController>().Init(_agentConfigBase.AnimatorOverrideController);

        HealthPool.FullRestore();

        HealthUpdated();
    }

    private void Start()
    {
        HealthUpdated();
    }

    private void OnEnable()
    {
        HealthPool.FullRestore();
    }

    public override void OnDisable()
    {
        if(_agentConfigBase != null)
        {
            SubtractStats(_agentConfigBase.StatsValues);
            _agentConfigBase = null;
        }

        base.OnDisable();
    }

    public void TakeDamage(int damage)
    {
        if (damage > 0)
        {
            HealthPool.CurrentValue -= damage;
            HealthUpdated();
            if (HealthPool.CurrentValue <= 0)
            {
                HealthPool.CurrentValue = 0;
                Die();
            }
        }
    }

    public StatsValue GetStatsValue(Statistic statisticToGet)
    {
        return _stats.Get(statisticToGet);
    }

    public ValuePool GetHealthPool()
    {
        return HealthPool;
    }

    public int GetDamage()
    {
        int damage = GetStatsValue(Statistic.Damage).Integer_value;
        return damage;
    }

    private void Die()
    {
        if (isDead == true)
        {
            return;
        }
        OnDieEvent?.Invoke();
        isDead = true;
        _agentAnimationController.SetState(AnimationState.Death);

        gameObject.SetActive(false);
    }

    private void HealthUpdated()
    {
        OnHealthChangedEvent?.Invoke(HealthPool);
    }

    public void AddStats(List<StatsValue> statsValues)
    {
        for (int i = 0; i < statsValues.Count; i++)
        {
            StatAdd(statsValues[i]);
        }
    }

    private void StatAdd(StatsValue statsValue)
    {
        _stats.Sum(statsValue);
    }

    public void SubtractStats(List<StatsValue> statsValues)
    {
        for (int i = 0; i < statsValues.Count; i++)
        {
            SubtractStats(statsValues[i]);
        }
    }

    private void SubtractStats(StatsValue statsValue)
    {
        _stats.Subtract(statsValue);
    }
}
