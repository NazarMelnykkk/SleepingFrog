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

    public override Agent Create(Transform targetObject)
    {
        PoolableObject newItemGO = pool.GetObject();

        Agent newAgent = newItemGO.GetComponent<Agent>();

        newAgent.transform.SetParent(targetObject);

        return newAgent;
    }
}
