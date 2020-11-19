using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnterBossAreas : MonoBehaviour
{
    [SerializeField] private ScriptableBoss boss = null;
    [SerializeField] private float[] attackChances = null;
    [SerializeField] private int colliderPriority = 0;
    private float[] defaultAttackChances;

    private void Start()
    {
        defaultAttackChances = boss.attackChances;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (boss.colliderPriority <= colliderPriority)
        {
            boss.attackChances = attackChances;
            boss.colliderPriority = colliderPriority;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        OnReset();
    }

    private void OnDisable()
    {
        OnReset();
    }

    private void OnReset()
    {
        boss.attackChances = defaultAttackChances;
        boss.colliderPriority = 0;
    }
}
