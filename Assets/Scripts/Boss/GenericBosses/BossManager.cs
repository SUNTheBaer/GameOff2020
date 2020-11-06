using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackChances
{
    public float chance;
    public string attackName;

    public AttackChances(float chance, string attackName)
    {
        this.chance = chance;
        this.attackName = attackName;
    }
}

public class BossManager
{
    public float bossHealth;
    public float bossAttackDamage;
    public float playerAttackDamage;
}
