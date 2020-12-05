#pragma warning disable 0414
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterBossAreas : MonoBehaviour
{
    [SerializeField] private BossProbability boss = null;
    [SerializeField] private string[] attackNames = null;
    public float[] attackChances = null;
    private float[] defaultAttackChances;
    [SerializeField] private int colliderPriority = 0;

    private void Start()
    {
        defaultAttackChances = new float[attackChances.Length];
        for (int i = 0; i < attackChances.Length; i++)
            defaultAttackChances[i] = attackChances[i];
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (boss.colliderPriority <= colliderPriority)
            {
                boss.attackChances = attackChances;
                boss.colliderPriority = colliderPriority;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            OnReset();
    }

    private void OnDisable()
    {
        OnReset();
    }

    private void OnReset()
    {
        boss.colliderPriority = 0;
    }

    public void ResetProbabilities()
    {
        for (int i = 0; i < attackChances.Length; i++)
            attackChances[i] = defaultAttackChances[i];
    }
}