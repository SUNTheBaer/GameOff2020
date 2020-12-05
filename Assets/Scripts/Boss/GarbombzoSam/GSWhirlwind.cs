using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GSWhirlwind : MonoBehaviour
{
    [SerializeField] private GarbombzoSam garbombzoSam = null;
    [SerializeField] private float attackDamage = 0;
    [SerializeField] private float knockbackForce = 0;
    private Vector2 knockbackDirection = Vector2.zero;
    [SerializeField] private float knockbackTime = 0;
    [SerializeField] private float attackLength = 0;
    [SerializeField] private float waitForNextAttack = 0;
    private float t = 0;
    private bool attackOngoing = false;

    private void Update() {
        if (attackOngoing)
        {
            t += Time.deltaTime;

            if (t > attackLength)
            {
                garbombzoSam.anim.SetBool("whirlwind", false);
                Invoke("EndWhirlwind", waitForNextAttack);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            knockbackDirection = (Vector2)(garbombzoSam.player.transform.position - transform.position);
            garbombzoSam.playerScript.playerMovement.Knockback(knockbackDirection, knockbackForce, knockbackTime);
            StartCoroutine(garbombzoSam.playerScript.playerCollision.TakeDamage(attackDamage));
        }
    }

    public void StartWhirlwind()
    {
        t = 0;
        garbombzoSam.anim.SetBool("whirlwind", true);
        attackOngoing = true;
    }

    private void EndWhirlwind()
    {
        t = 0;
        garbombzoSam.PickAttack();
        attackOngoing = false;
    }
}
