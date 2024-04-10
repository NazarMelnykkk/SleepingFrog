using System;

public class MiscEvents 
{
    public event Action OnEnemyDeathEvent;

    public void EnemyDeath()
    {
        OnEnemyDeathEvent?.Invoke();
    }
}
