using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GSPunch : MonoBehaviour
{
    [SerializeField] private GarbombzoSam garbombzoSam = null;
    [SerializeField] private float attackDamage = 0;
    [SerializeField] private float knockbackForce = 0;
    private Vector2 knockbackDirection = Vector2.zero;
    [SerializeField] private float knockbackTime = 0;
    [SerializeField] private float waitForNextAttack = 0;
    [SerializeField] private float reelTime = 0;
    public float hammerMaxAngle = 0;
    private bool alreadyHit = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ZeeAttack"))
            StartCoroutine(garbombzoSam.TakeDamage(garbombzoSam.playerScript.damage));
        
        if (other.gameObject.CompareTag("Player") && !alreadyHit)
        {
            StartCoroutine(garbombzoSam.playerScript.playerCollision.TakeDamage(attackDamage));
            garbombzoSam.playerScript.playerMovement.Knockback(knockbackDirection, knockbackForce, knockbackTime);
            alreadyHit = true;
        }
    }

    public IEnumerator StartPunch()
    {
        garbombzoSam.swingingHammer = true;
        garbombzoSam.time = 0;
        garbombzoSam.anim.SetTrigger("hammer reel");

        yield return new WaitForSeconds(reelTime); //Initial hit wait time
        
        garbombzoSam.anim.SetTrigger("hammer punch");
        knockbackDirection = new Vector2(Mathf.Cos((garbombzoSam.angleAxis - 90) * Mathf.Deg2Rad),
            Mathf.Sin((garbombzoSam.angleAxis - 90) * Mathf.Deg2Rad));

        yield return new WaitForSeconds(.833f); //Length of punch anim

        garbombzoSam.anim.SetTrigger("hammer reel");
        alreadyHit = false;

        yield return new WaitForSeconds(.333f); //Speed up second hit
        
        garbombzoSam.anim.SetTrigger("hammer punch");
        knockbackDirection = new Vector2(Mathf.Cos((garbombzoSam.angleAxis - 90) * Mathf.Deg2Rad),
            Mathf.Sin((garbombzoSam.angleAxis - 90) * Mathf.Deg2Rad));

        yield return new WaitForSeconds(.833f); //Length of punch anim

        garbombzoSam.time = 0;
        garbombzoSam.swingingHammer = false;

        yield return new WaitForSeconds(waitForNextAttack);

        garbombzoSam.PickAttack();
        alreadyHit = false;
    }
}
