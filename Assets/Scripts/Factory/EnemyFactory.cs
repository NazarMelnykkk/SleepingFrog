using UnityEngine;

public class EnemyFactory : AgentFactory
{
    public static EnemyFactory Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        pool = ObjectPool.CreateInstance(_AgentPrefab, _poolSize);
    }

    public override AIAgentBase Create(Transform targetObject)
    {
        PoolableObject newItemGO = pool.GetObject();

        AIEnemy newAgent = newItemGO.GetComponent<AIEnemy>();

        newAgent.transform.SetParent(targetObject);

        return newAgent;
    }
}
