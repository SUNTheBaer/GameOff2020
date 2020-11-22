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
    public float bossMaxHealth;
    public float bossCurrentHealth;
    public float bossAttackDamage;
    public float playerAttackDamage;
    public Vector2 playerPosition;
    public Vector2 bossPosition;
    public Vector2 knockback;
    public float knockbackTime;
    public float knockbackForce;
    public Vector2 knockbackDirection;
}
