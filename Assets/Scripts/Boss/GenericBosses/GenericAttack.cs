#pragma warning disable 0414
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericAttack : MonoBehaviour
{
    //[SerializeField] private BossName bossName = null;
    [SerializeField] private float attackDamage = 0;
    [SerializeField] private float knockbackForce = 0;
    private Vector2 knockbackDirection = Vector2.zero;
    [SerializeField] private float knockbackTime = 0;
    [SerializeField] private float waitForNextAttack = 0;
    //private bool alreadyHit = false; only need this variable if you want the attack to only hit once

    private void OnTriggerEnter2D(Collider2D other)
    {
        /*if (other.gameObject.CompareTag("Player") && !alreadyHit)
        {
            knockbackDirection = figure it out;
            bossName.playerScript.playerMovement.Knockback(knockbackDirection, knockbackForce, knockbackTime);
            StartCoroutine(bossName.playerScript.playerCollision.TakeDamage(attackDamage));
            alreadyHit = true;
        }*/
    }

    public IEnumerator StartAttackOne()
    {
        //bossName.anim.SetTrigger("AnimName");

        yield return new WaitForSeconds(waitForNextAttack);

        //bossName.PickAttack();
        //alreadyHit = false;
    }
}
