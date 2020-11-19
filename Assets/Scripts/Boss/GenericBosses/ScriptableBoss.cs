using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Boss", menuName = "Boss")]
public class ScriptableBoss : ScriptableObject
{
    public float bossMaxHealth;
    public float bossCurrentHealth;
    public string bossName;
    public float[] attackChances;
    public int colliderPriority;
}
