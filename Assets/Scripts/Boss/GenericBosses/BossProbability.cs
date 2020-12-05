using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossProbability", menuName = "BossProbability")]
public class BossProbability : ScriptableObject
{
    public float[] attackChances;
    public int colliderPriority;
}
