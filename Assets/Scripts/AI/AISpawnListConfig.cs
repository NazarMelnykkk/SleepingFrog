using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AISpawnType
{
    public AgentConfigBase AIType;
    public int weight = 1;
}


[CreateAssetMenu(fileName = "AISpawnList", menuName = "AIGroup/AISpawnList")]
public class AISpawnListConfig : ScriptableObject
{
    public List<AISpawnType> AITypes;
    public int TotalWeight;

    [ContextMenu("Calculate weights")]
    public void CalculateTotalWeight()
    {
        TotalWeight = 0;
        foreach (AISpawnType AIType in AITypes)
        {
            TotalWeight += AIType.weight;
        }
        TotalWeight += 1;
    }

    public string GetSpawnName()
    {
        AgentConfigBase AIData = GetSpawn();
        return AIData.name;
    }

    public AgentConfigBase GetSpawn()
    {
        AgentConfigBase toSpawn = AITypes[0].AIType;
        int roll = UnityEngine.Random.Range(0, TotalWeight);
        int i = 0;

        while (roll > 0)
        {
            roll -= AITypes[i].weight;
            toSpawn = AITypes[i].AIType;
            i++;
        }

        return toSpawn;
    }
}
