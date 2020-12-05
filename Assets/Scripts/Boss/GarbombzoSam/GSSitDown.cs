using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GSSitDown : MonoBehaviour
{
    [SerializeField] private GarbombzoSam garbombzoSam = null;
    [SerializeField] private float attackDamage = 0;
    [SerializeField] private float knockbackForce = 0;
    private Vector2 knockbackDirection = Vector2.zero;
    [SerializeField] private float knockbackTime = 0;
    [SerializeField] private float waitForNextAttack = 0;
    private bool alreadyHit = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !alreadyHit)
        {
            knockbackDirection = (Vector2)(garbombzoSam.player.transform.position - transform.position);
            garbombzoSam.playerScript.playerMovement.Knockback(knockbackDirection, knockbackForce, knockbackTime);
            StartCoroutine(garbombzoSam.playerScript.playerCollision.TakeDamage(attackDamage));
            alreadyHit = true;
        }
    }

    public IEnumerator StartSitDown()
    {
        garbombzoSam.anim.SetTrigger("sit yourself");

        yield return new WaitForSeconds(waitForNextAttack);

        garbombzoSam.PickAttack();
        alreadyHit = false;
    }
}
