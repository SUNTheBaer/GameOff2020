using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Boss", menuName = "Boss")]
public class ScriptableBoss : ScriptableObject
{
    public float bossHealth;
    public string bossName;
}
