using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentFactory : MonoBehaviour
{

    [SerializeField] protected PoolableObject _AgentPrefab;
    [SerializeField] protected int _poolSize = 30;

    public ObjectPool pool;

    public virtual AIAgentBase Create(Transform targetObject)
    {
        PoolableObject newItemGO = pool.GetObject();

        AIAgentBase newAgent = newItemGO.GetComponent<AIAgentBase>();

        newAgent.transform.SetParent(targetObject);

        return newAgent;
    }
}
