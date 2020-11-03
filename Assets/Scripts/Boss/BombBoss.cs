using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBoss : MonoBehaviour
{
    private int damage = 0;
    [SerializeField] private BossManager bossManager = null;
    //private int staggerTime;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            bossManager.DealDamage(damage);
    }

    //private void SwipeAttack
    //...
    //...
}
