using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[CreateAssetMenu(fileName = "Agent", menuName = "Agent/AgentBase")]

public class AgentConfigBase : ScriptableObject
{
    public List<StatsValue> StatsValues;

    public AnimatorOverrideController AnimatorOverrideController;
}
